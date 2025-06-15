using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces
{
    public interface IItemInPurchaseSaleTransactionCommandService
    {
        Task<Guid> AddItemInPurchaseSaleTransactionAsync(AddItemInPurchaseSaleTransactionCommand command, CancellationToken cancellationToken);
    }
}
