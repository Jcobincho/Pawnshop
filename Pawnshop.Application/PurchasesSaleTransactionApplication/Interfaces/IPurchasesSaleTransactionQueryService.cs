using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces
{
    public interface IPurchasesSaleTransactionQueryService
    {
        Task<PurchaseSaleTransaction> GetPurchaseSateTransactionByIdAsync(Guid purchaseSateTransactionId, CancellationToken cancellationToken);
    }
}
