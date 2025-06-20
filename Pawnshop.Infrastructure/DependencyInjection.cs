﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.JsonWebTokenApplication.Interfaces;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Infrastructure.Persistance.Extensions;
using Pawnshop.Infrastructure.Services.ClientsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemDetailsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemCategoriesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.JsonWebTokenInfrastructure.Services;
using Pawnshop.Infrastructure.Services.UserClaimsDataProvidesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.UsersInfrastructure.Services;
using Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Infrastructure.Services.CompanyEmailsInfrastructure.Services;
using Pawnshop.Application.CryptographyApplication.Interface;
using Pawnshop.Infrastructure.Services.CryptographyInfrastructure.Services;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Services;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Infrastructure.Services.ItemValuationsInfrastructure.Services;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Services;
using Pawnshop.Application.Common.Behaviors;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Infrastructure.Services.ItemInPurchaseSaleTransactionInfrastructure.Services;
using Pawnshop.Application.ItemHistoriesApplication.Producers;
using Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Producers;

namespace Pawnshop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.DatabaseConfiguration(configuration);
        services.AuthorizationSettings(configuration);
        services.AddMassTransitWithRabbitMq(configuration);


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

        // MassTransit create item history with valuation
        services.AddScoped<IItemHistoryEventPublisher, ItemHistoryEventPublisher>();

        // Item in purchase and sale transaction service
        services.AddScoped<IItemInPurchaseSaleTransactionCommandService, ItemInPurchaseSaleTransactionService>();
        services.AddScoped<IItemInPurchaseSaleTransactionQueryService, ItemInPurchaseSaleTransactionService>();

        // Purchase and sale transactions services
        services.AddScoped<IPurchasesSaleTransactionCommandService, PurchasesSaleTransactionService>();
        services.AddScoped<IPurchasesSaleTransactionQueryService, PurchasesSaleTransactionService>();

        // Item valuation services
        services.AddScoped<IItemValuationsCommandService, ItemValuationsService>();
        services.AddScoped<IItemValuationsQueryService, ItemValuationsService>();

        // Item history services
        services.AddScoped<IItemHistoriesCommandService, ItemHistoriesService>();
        services.AddScoped<IItemHistoriesQueryService, ItemHistoriesService>();

        // CompanyEmail services
        services.AddScoped<ICompanyEmailsCommandService, CompanyEmailsService>();
        services.AddScoped<ICompanyEmailsQueryService, CompanyEmailsService>();

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

        // Cryptography service
        services.AddScoped<ICryptographyService, CryptographyService>();

        // Register pipeline behavior for getting user id from claims
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserIdPipelineBehavior<,>));

        return services;
    }
}