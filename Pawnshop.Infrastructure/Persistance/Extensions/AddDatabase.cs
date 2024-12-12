using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Domain.Entities;
using Pawnshop.Infrastructure.Persistance.Seeders;

namespace Pawnshop.Infrastructure.Persistance.Extensions;

public static class AddDatabase
{
    public static IServiceCollection DatabaseConfiguration(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.AddDbContext<DbContext>(options => options.UseNpgsql(configurationManager.GetConnectionString("Database"),
            optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AddDatabase).Assembly.FullName)));

        services.AddHostedService<InitialDatabaseSeeder>();
        
        services.AddIdentity<Users, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}