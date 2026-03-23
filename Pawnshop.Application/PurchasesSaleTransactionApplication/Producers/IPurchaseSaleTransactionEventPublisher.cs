using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Consumers.GenerateTransactionReport;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Producers
{
    public interface IPurchaseSaleTransactionEventPublisher
    {
        Task GenerateAgreementPublishAsync(GenerateAgreementEvent @event, CancellationToken cancellationToken);
        Task GenerateTransactionReportPublishAsync(GenerateTransactionReportEvent @event, CancellationToken cancellationToken);
    }
}
