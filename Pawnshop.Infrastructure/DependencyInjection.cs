using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Domain.Entities;
using Pawnshop.Infrastructure.Persistance.Extensions;

namespace Pawnshop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.DatabaseConfiguration(configuration);
        
        services.AddSingleton<SignInManager<Users>>();
        services.AddScoped<UserManager<Users>>();
        
        return services;
    }
}