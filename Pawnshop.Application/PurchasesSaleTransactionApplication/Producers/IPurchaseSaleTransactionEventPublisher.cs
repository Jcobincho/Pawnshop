using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Producers
{
    public interface IPurchaseSaleTransactionEventPublisher
    {
        Task GenerateAgreementPublishAsync(GenerateAgreementEvent @event, CancellationToken cancellationToken);
    }
}
