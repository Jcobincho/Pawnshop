using Microsoft.AspNetCore.Identity;
using Pawnshop.Domain.Roles;

namespace Pawnshop.Infrastructure.Persistance.Seeders;

public static class RolesSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager, DbContext context, CancellationToken cancellationToken = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var roles = UserRoles.GetRoles();
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }

        await transaction.CommitAsync(cancellationToken);
    }
}