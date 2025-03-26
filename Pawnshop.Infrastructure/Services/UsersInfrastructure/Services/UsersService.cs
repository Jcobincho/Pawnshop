using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.JsonWebTokenApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Commands.RefreshToken;
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
                bool isEmployeeExists = await _dbContext.Employee.AnyAsync(x => x.Id == command.EmployeeId, cancellationToken);

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

            IdentityResult addClaims = await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()));

            if (!addClaims.Succeeded) throw new BadRequestException("Add roles failed.");

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
                                                  .SingleOrDefaultAsync(x => x.Id == command.UserIdFromClaims, cancellationToken)
                                                ?? throw new NotFoundException("Invalid refresh token.");

            var token = user.RefreshToken.FirstOrDefault(x => x.Token == command.RefreshToken);

            if (token is null) throw new NotFoundException("Invalid refresh token.");
            

            user.DeleteRefreshToken(token);
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _signInManager.SignOutAsync();
        }
    }
}
