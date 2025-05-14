using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Interfaces;
<<<<<<< HEAD
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
=======
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.JsonWebTokenApplication.Interfaces;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Infrastructure.Behaviors;
using Pawnshop.Infrastructure.Persistance.Extensions;
using Pawnshop.Infrastructure.Services.ClientsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemDetailsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemCategoriesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.JsonWebTokenInfrastructure.Services;
using Pawnshop.Infrastructure.Services.UserClaimsDataProvidesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.UsersInfrastructure.Services;
using Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services;

namespace Pawnshop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.DatabaseConfiguration(configuration);
        services.AuthorizationSettings(configuration);
        
        services.AddScoped<SignInManager<Users>>();
        services.AddScoped<UserManager<Users>>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 6;
            
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5);
            
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        });
        
        services.AddHttpContextAccessor();

        // Interfaces Dependency Injection

        // Itemdetails service
        services.AddScoped<IItemDetailsCommandService, ItemDetailsService>();
        services.AddScoped<IItemDetailsQueryService, ItemDetailsService>();

        // Workplaces service
        services.AddScoped<IWorkplacesCommandService, WorkplacesService>();
        services.AddScoped<IWorkplacesQueryService, WorkplacesService>();

        // Clients services
        services.AddScoped<IClientsCommandService, ClientsService>();
        services.AddScoped<IClientsQueryService, ClientsService>();

        // Users services
        services.AddScoped<IUsersCommandService, UsersService>();
        services.AddScoped<IUsersQueryService, UsersService>();

        // Employees services
        services.AddScoped<IEmployeesCommandService, EmployeesService>();
        services.AddScoped<IEmployeesQueryService, EmployeesService>();

        //ItemCategories services
        services.AddScoped<IItemCaterogoriesCommandService, ItemCategoriesService>();
        services.AddScoped<IItemCategoriesQueryService, ItemCategoriesService>();

        // JsonWebToken service
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();

        // User claims data provicer service
        services.AddScoped<IUserClaimsDataProviderService, UserClaimsDataProviderService>();

        // Register pipeline behavior for getting user id from claims
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserIdPipelineBehavior<,>));

        return services;
    }
}