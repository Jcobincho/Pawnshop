using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.UsersApplication.Dto;
using Pawnshop.Application.UsersApplication.Dto.DtoExtension;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.UsersInfrastructure.Services
{
    internal sealed class UsersQueryService : IUsersQueryService
    {
        private readonly DbContext _dbContext;
        private readonly UserManager<Users> _userManager;

        public UsersQueryService(DbContext dbContext, UserManager<Users> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
    }
}
