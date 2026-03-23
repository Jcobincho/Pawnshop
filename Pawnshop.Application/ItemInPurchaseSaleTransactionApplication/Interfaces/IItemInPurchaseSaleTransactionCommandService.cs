using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.DeleteItemInPurchaseSaleTransaction;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces
{
    public interface IItemInPurchaseSaleTransactionCommandService
    {
        Task<Guid> AddItemInPurchaseSaleTransactionAsync(AddItemInPurchaseSaleTransactionCommand command, CancellationToken cancellationToken);
        Task DeleteItemInPurchaseSaleTransactionAsync(DeleteItemInPurchaseSaleTransactionCommand command, CancellationToken cancellationToken);
    }
}
