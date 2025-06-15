using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces
{
    public interface IItemInPurchaseSaleTransactionQueryService
    {
        Task<ItemInPurchaseSaleTransaction> GetItemInPurchaseSaleTransactionAsync(Guid ItemInPurchaseSaleTransactionId, CancellationToken cancellationToken);
    }
}
