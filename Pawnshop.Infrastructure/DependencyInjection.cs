using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.Common.Behaviors;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CryptographyApplication.Interface;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Producers;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.JsonWebTokenApplication.Interfaces;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Producers;
using Pawnshop.Application.UserClaimsDataProviderApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Infrastructure.Persistance.Extensions;
using Pawnshop.Infrastructure.Services.ClientsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.CompanyEmailsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.CryptographyInfrastructure.Services;
using Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.FileStorageInfrastructure.FileStorage.Services;
using Pawnshop.Infrastructure.Services.FileStorageInfrastructure.PurchaseSaleTransactionAgreementStorage.Services;
using Pawnshop.Infrastructure.Services.ItemCategoriesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemDetailsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Producers;
using Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemInPurchaseSaleTransactionInfrastructure.Services;
using Pawnshop.Infrastructure.Services.ItemValuationsInfrastructure.Services;
using Pawnshop.Infrastructure.Services.JsonWebTokenInfrastructure.Services;
using Pawnshop.Infrastructure.Services.PdfGeneratorInfrastructure.Services;
using Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Producers;
using Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Services;
using Pawnshop.Infrastructure.Services.UserClaimsDataProvidesInfrastructure.Services;
using Pawnshop.Infrastructure.Services.UsersInfrastructure.Services;
using Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services;

using Pawnshop.Application.NotificationApplication.Interfaces;
using Pawnshop.Infrastructure.Services.NotificationInfrastructure.Services;

namespace Pawnshop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.DatabaseConfiguration(configuration);
        services.AuthorizationSettings(configuration);
        services.AddMassTransitWithRabbitMq(configuration);
        services.AddFileStorageConfiguration(configuration);
        services.HandlebarsRegisterSettings();

        services.AddSignalR();


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

        // Notification services
        services.AddScoped<INotificationService, NotificationService>();

        // MassTransit create item history with valuation
        services.AddScoped<IItemHistoryEventPublisher, ItemHistoryEventPublisher>();

        // MassTransit generate purchase sale trancastion agreement
        services.AddScoped<IPurchaseSaleTransactionEventPublisher, PurchaseSaleTransactionEventPublisher>();

        // File agreement service
        services.AddScoped<IPurchaseSaleTransactionAgreementCommandService, PurchaseSaleTransactionAgreementCommandService>();
        services.AddScoped<IPurchaseSaleTransactionAgreementQueryService, PurchaseSaleTransactionAgreementQueryService>();

        // File storage service
        services.AddScoped<IFileStorageQueryService, FileStorageQueryService>();
        services.AddScoped<IFileStorageEditService, FileStorageEditService>();

        // Item in purchase and sale transaction service
        services.AddScoped<IItemInPurchaseSaleTransactionCommandService, ItemInPurchaseSaleTransactionCommandService>();
        services.AddScoped<IItemInPurchaseSaleTransactionQueryService, ItemInPurchaseSaleTransactionQueryService>();

        // Purchase and sale transactions services
        services.AddScoped<IPurchasesSaleTransactionCommandService, PurchasesSaleTransactionCommandService>();
        services.AddScoped<IPurchasesSaleTransactionQueryService, PurchasesSaleTransactionQueryService>();

        // Item valuation services
        services.AddScoped<IItemValuationsCommandService, ItemValuationsCommandService>();
        services.AddScoped<IItemValuationsQueryService, ItemValuationsQueryService>();

        // Item history services
        services.AddScoped<IItemHistoriesCommandService, ItemHistoriesCommandService>();
        services.AddScoped<IItemHistoriesQueryService, ItemHistoriesQueryService>();

        // CompanyEmail services
        services.AddScoped<ICompanyEmailsCommandService, CompanyEmailsCommandService>();
        services.AddScoped<ICompanyEmailsQueryService, CompanyEmailsQueryService>();

        // Itemdetails service
        services.AddScoped<IItemDetailsCommandService, ItemDetailsCommandService>();
        services.AddScoped<IItemDetailsQueryService, ItemDetailsQueryService>();

        // Workplaces service
        services.AddScoped<IWorkplacesCommandService, WorkplacesCommandService>();
        services.AddScoped<IWorkplacesQueryService, WorkplacesQueryService>();

        // Clients services
        services.AddScoped<IClientsCommandService, ClientsCommandService>();
        services.AddScoped<IClientsQueryService, ClientsQueryService>();

        // Users services
        services.AddScoped<IUsersCommandService, UsersCommandService>();
        services.AddScoped<IUsersQueryService, UsersQueryService>();

        // Employees services
        services.AddScoped<IEmployeesCommandService, EmployeesCommandService>();
        services.AddScoped<IEmployeesQueryService, EmployeesQueryService>();

        //ItemCategories services
        services.AddScoped<IItemCaterogoriesCommandService, ItemCaterogoriesCommandService>();
        services.AddScoped<IItemCategoriesQueryService, ItemCategoriesQueryService>();

        // JsonWebToken service
        services.AddScoped<IJsonWebTokenService, JsonWebTokenService>();

        // User claims data provicer service
        services.AddScoped<IUserClaimsDataProviderService, UserClaimsDataProviderService>();

        // Cryptography service
        services.AddScoped<ICryptographyService, CryptographyService>();

        // Pdf service
        services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();
        services.AddScoped<IReportGeneratorService, ReportGeneratorService>();

        // Register pipeline behavior for getting user id from claims
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserIdPipelineBehavior<,>));

        return services;
    }
}