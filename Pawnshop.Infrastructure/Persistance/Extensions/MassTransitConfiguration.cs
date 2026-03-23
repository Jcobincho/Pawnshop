using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Consumers;
using Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Consumers;

namespace Pawnshop.Infrastructure.Persistance.Extensions
{
    public static class MassTransitConfiguration
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<AddItemHistoryAndItemValuationConsumer>();
                x.AddConsumer<GenerateAgreementCustomer>();
                x.AddConsumer<GenerateTransactionReportConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:Host"], h =>
                    {
                        h.Username(configuration["RabbitMq:Username"]);
                        h.Password(configuration["RabbitMq:Password"]);
                    });

                    cfg.ReceiveEndpoint("item-history-and-item-valuation-queue", e =>
                    {
                        e.ConfigureConsumer<AddItemHistoryAndItemValuationConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("generate-agreement-queue", e =>
                    {
                        e.ConfigureConsumer<GenerateAgreementCustomer>(context);
                    });

                    cfg.ReceiveEndpoint("generate-transaction-report-queue", e =>
                    {
                        e.ConfigureConsumer<GenerateTransactionReportConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
