using MassTransit;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Producers;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Producers
{
    internal sealed class PurchaseSaleTransactionEventPublisher : IPurchaseSaleTransactionEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PurchaseSaleTransactionEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task GenerateAgreementPublishAsync(GenerateAgreementEvent @event, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
