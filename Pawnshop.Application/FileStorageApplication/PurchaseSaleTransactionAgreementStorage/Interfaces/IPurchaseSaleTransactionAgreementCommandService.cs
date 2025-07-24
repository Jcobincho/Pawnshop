using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Commands.AddPurchaseSaleTransactionAgreement;

namespace Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Interfaces
{
    public interface IPurchaseSaleTransactionAgreementCommandService
    {
        Task<Guid> AddPurchaseSaleTransactionAgreementAsync(AddPurchaseSaleTransactionAgreementCommand command, CancellationToken cancellationToken);
    }
}
