using Pawnshop.Application.ItemDetailsApplication.Dto;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto;
using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces
{
    public interface IItemInPurchaseSaleTransactionQueryService
    {
        Task<ItemInPurchaseSaleTransaction> GetItemInPurchaseSaleTransactionAsync(Guid ItemInPurchaseSaleTransactionId, CancellationToken cancellationToken);
        Task<List<ItemInPurchaseSaleTransactionDto>> GetItemsForPurchaseSaleTransactionAsync(Guid purchaseSaleTransactionId, CancellationToken cancellationToken);
    }
}
