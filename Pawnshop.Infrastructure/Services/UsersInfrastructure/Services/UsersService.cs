using FluentValidation.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.JsonWebTokenApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.EditUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Commands.RefreshToken;
using Pawnshop.Application.UsersApplication.Dto;
using Pawnshop.Application.UsersApplication.Dto.DtoExtension;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Domain.AuthTokens;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;
using Pawnshop.Domain.Roles;
using System.Security.Claims;

namespace Pawnshop.Infrastructure.Services.UsersInfrastructure.Services
{
    internal sealed class UsersService : IUsersCommandService, IUsersQueryService
    {

        private readonly DbContext _dbContext;
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly IJsonWebTokenService _jsonWebTokenService;

        public UsersService(DbContext dbContext, SignInManager<Users> signInManager, UserManager<Users> userManager, IJsonWebTokenService jsonWebTokenService)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _jsonWebTokenService = jsonWebTokenService;
        }

        public async Task<Guid> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken)
        {
            bool isEmailUnique = await _userManager.Users.AnyAsync(x => x.Email == command.Email, cancellationToken);
            bool isUserNameUnique = await _userManager.Users.AnyAsync(x => x.UserName == command.UserName, cancellationToken);

            if (isEmailUnique) throw new BadRequestException("Email already exists.");
            if (isUserNameUnique) throw new BadRequestException("Username already exists.");

            Users newUser = new Users
            {
                UserName = command.UserName,
                Email = command.Email
            };

            if(command.EmployeeId != Guid.Empty)
            {
                bool isEmployeeExists = await _dbContext.Employees.AnyAsync(x => x.Id == command.EmployeeId, cancellationToken);

                if(isEmployeeExists)
                {
                    newUser.EmployeesId = command.EmployeeId;
                }
            }

            IdentityResult createUser = await _userManager.CreateAsync(newUser, command.Password);

            if (!createUser.Succeeded) throw new CreateUserException(createUser.Errors);

            IdentityResult addUserRoles;

            List<string> userRolesToAdd = new();

            if(command.UserRoles.Any())
            { 
                foreach(var role in command.UserRoles)
                {
                    bool isRoleValid = UserRoles.GetRoles().Any(x => x.Name == role);

                    if(isRoleValid)
                    {
                        userRolesToAdd.Add(role);
                    }
                }
            }

            if(userRolesToAdd.Any())
            {
                addUserRoles = await _userManager.AddToRolesAsync(newUser, userRolesToAdd);

                if (!addUserRoles.Succeeded) throw new BadRequestException("Add roles failed.");
            }

            IdentityResult addNameIdentifierClaims = await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()));

            if (!addNameIdentifierClaims.Succeeded) throw new BadRequestException("Add claims failed.");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newUser.Id;
        }

        public async Task<JsonWebToken> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(command.Username);

            if(user == null) 
                throw new NotFoundException("Login or password incorrect.");

            var passwordVerification = await _signInManager.CheckPasswordSignInAsync(user, command.Password, true);

            if(!passwordVerification.Succeeded)
                throw new NotFoundException("Login or password incorrect.");
            

            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var jwtToken = _jsonWebTokenService.GenerateJsonWebToken(user, userRoles, userClaims);
            var refreshToken = _jsonWebTokenService.GenerateRefreshToken();

            jwtToken.RefreshToken = refreshToken;

            _jsonWebTokenService.DeleteExpiresRefreshToken(user);

            user.AddRefreshToken(refreshToken);

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return jwtToken;
        }

        public async Task<JsonWebToken> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(x => x.RefreshToken)
                                               .SingleOrDefaultAsync(x => x.RefreshToken.Any(token => token.Token == command.RefreshToken), cancellationToken)
                                               ?? throw new BadRequestException("Invalid refresh token.");

            var currentToken = user.RefreshToken.Single(x => x.Token == command.RefreshToken);

            if (currentToken.IsExpired) throw new BadRequestException("Invalid refresh token.");

            user.DeleteRefreshToken(currentToken);

            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var jwtToken = _jsonWebTokenService.GenerateJsonWebToken(user, userRoles, userClaims);
            var newRefreshToken = _jsonWebTokenService.GenerateRefreshToken();

            jwtToken.RefreshToken = newRefreshToken;
            _jsonWebTokenService.DeleteExpiresRefreshToken(user);
            user.AddRefreshToken(newRefreshToken);

            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return jwtToken;

        }

        public async Task LogoutAsync(LogoutCommand? command, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.Include(x => x.RefreshToken)
                                                  .SingleOrDefaultAsync(x => x.Id == command.UserIdFromClaims, cancellationToken);
                                                

            if (user == null)
            {
                throw new NotFoundException("Invalid refresh token.");
            }

            var token = user.RefreshToken.FirstOrDefault(x => x.Token == command.RefreshToken);

            if (token is null) throw new NotFoundException("Invalid refresh token.");
            

            user.DeleteRefreshToken(token);
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _signInManager.SignOutAsync();
        }

        public async Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var userToEdit = await _userManager.FindByIdAsync(command.UserId.ToString());

            if (userToEdit == null) throw new NotFoundException("User identifier is incorrect.");

            if (userToEdit.UserName != command.UserName)
            {
                bool isUserNameTaken = await _userManager.Users.AnyAsync(u => u.UserName == command.UserName, cancellationToken);
                if (isUserNameTaken)
                    throw new BadRequestException("Username already exists.");
                userToEdit.UserName = command.UserName;
            }

            if (userToEdit.Email != command.Email)
            {
                bool isEmailTaken = await _userManager.Users.AnyAsync(u => u.Email == command.Email, cancellationToken);
                if (isEmailTaken)
                    throw new BadRequestException("Email already exists.");
                userToEdit.Email = command.Email;
            }

            if (command.EmployeeId != Guid.Empty)
            {
                bool employeeExists = await _dbContext.Employees.AnyAsync(e => e.Id == command.EmployeeId, cancellationToken);
                if (!employeeExists)
                    throw new BadRequestException("Employee not found.");
                userToEdit.EmployeesId = command.EmployeeId;
            }
            else
            {
                userToEdit.EmployeesId = Guid.Empty;
            }

            if (!string.IsNullOrEmpty(command.Password))
            {
                var removeResult = await _userManager.RemovePasswordAsync(userToEdit);
                if (!removeResult.Succeeded)
                    throw new BadRequestException($"Failed to remove old password: {string.Join(", ", removeResult.Errors)}");

                var addResult = await _userManager.AddPasswordAsync(userToEdit, command.Password);
                if (!addResult.Succeeded)
                    throw new CreateUserException(addResult.Errors);
            }

            var currentRoles = await _userManager.GetRolesAsync(userToEdit);
            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(userToEdit, currentRoles);
                if (!removeResult.Succeeded)
                    throw new BadRequestException($"Failed to remove roles: {string.Join(", ", removeResult.Errors)}");
            }

            var validRoles = command.UserRoles.Where(role => UserRoles.GetRoles().Any(r => r.Name == role)).ToList();
            if (validRoles.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(userToEdit, validRoles);
                if (!addResult.Succeeded)
                    throw new BadRequestException($"Failed to add roles: {string.Join(", ", addResult.Errors)}");
            }

            var updateResult = await _userManager.UpdateAsync(userToEdit);
            if (!updateResult.Succeeded)
                throw new BadRequestException($"Failed to update user: {string.Join(", ", updateResult.Errors)}");

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<GetAllUsersDto>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .Include(u => u.Employee)
                .ToListAsync(cancellationToken);

            var userRoles = await _dbContext.UserRoles
                .Join(
                    _dbContext.Roles,
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur.UserId, RoleName = r.Name }
                )
                .GroupBy(ur => ur.UserId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(ur => ur.RoleName).ToList(),
                    cancellationToken
                );

            var result = users.Select(user =>
            {
                var dto = user.UserPraseToDto();
                dto.Roles = userRoles.GetValueOrDefault(user.Id) ?? new List<string>();
                return dto;
            }).ToList();

            return result;
        }

        public async Task UpdateEmployeeIdentifierAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.EmployeesId == employeeId, cancellationToken);

            if(user != null)
            {
                user.EmployeesId = employeeId;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(userId, cancellationToken);

            if (user == null)
                throw new NotFoundException("User does not exist.");

            _dbContext.Remove(user);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
