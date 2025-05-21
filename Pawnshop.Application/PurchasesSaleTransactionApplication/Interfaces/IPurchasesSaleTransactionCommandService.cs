using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces
{
    public interface IPurchasesSaleTransactionCommandService
    {
        Task<Guid> AddPurchaseSaleTransactionAsync(AddPurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken);
    }
}
