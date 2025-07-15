using MassTransit;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Consumers
{
    internal sealed class GenerateAgreementCustomer : IConsumer<GenerateAgreementEvent>
    {
        public async Task Consume(ConsumeContext<GenerateAgreementEvent> context)
        {
            
        }
    }
}
