using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pawnshop.Domain.Entities;

namespace Pawnshop.Infrastructure.Persistance.Seeders;

public class InitialDatabaseSeeder : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public InitialDatabaseSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService(typeof(DbContext)) as DbContext;
        
        var roles = scope.ServiceProvider.GetRequiredService(typeof(RoleManager<IdentityRole<Guid>>)) as RoleManager<IdentityRole<Guid>>;
        var userManager = scope.ServiceProvider.GetRequiredService(typeof(UserManager<Users>)) as UserManager<Users>;

        if (context is not null && roles is not null && userManager is not null)
        {
            await context.Database.MigrateAsync(cancellationToken);
            await RolesSeeder.SeedRolesAsync(roles, context);
            await BossAccountSeeder.SeedBossAccountAsync(userManager, context);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}