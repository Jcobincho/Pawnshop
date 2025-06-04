using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces
{
    public interface IPurchasesSaleTransactionCommandService
    {
        Task<Guid> AddPurchaseSaleTransactionAsync(AddPurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken);
        Task UpdatePurchaseSaleTransactionAsync(UpdatePurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken);
    }
}
