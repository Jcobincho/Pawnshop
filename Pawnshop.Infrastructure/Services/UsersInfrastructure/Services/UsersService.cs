using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.Users.Interfaces;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
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

        public UsersService(DbContext dbContext, SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
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
    }
}
