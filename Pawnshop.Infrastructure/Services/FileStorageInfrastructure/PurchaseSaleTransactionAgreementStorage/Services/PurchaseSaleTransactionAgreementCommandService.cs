using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Commands.AddPurchaseSaleTransactionAgreement;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Domain.Entities.FileStorage;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.FileStorageInfrastructure.PurchaseSaleTransactionAgreementStorage.Services
{
    internal sealed class PurchaseSaleTransactionAgreementCommandService : IPurchaseSaleTransactionAgreementCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;

        public PurchaseSaleTransactionAgreementCommandService(DbContext dbContext, IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService)
        {
            _dbContext = dbContext;
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
        }

        public async Task<Guid> AddPurchaseSaleTransactionAgreementAsync(AddPurchaseSaleTransactionAgreementCommand command, CancellationToken cancellationToken)
        {
            var isDocumentExist = await _purchasesSaleTransactionQueryService.IsPurchaseSaleTransactionExistAsync(command.PurchaseSaleTransactionId, cancellationToken);

            if (!isDocumentExist)
                throw new BadRequestException("Document doesn't exist.");

            var newAgreement = new PurchaseSaleTransactionAgreement
            {
                PurchaseSaleTransactionId = command.PurchaseSaleTransactionId,
                Symbol = command.Symbol,
                Url = command.Url,
                ContentType = command.ContentType,
                TotalBytes = command.TotalBytes,
                S3Key = command.S3Key,
            };

            await _dbContext.PurchaseSaleTransactionAgreements.AddAsync(newAgreement, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newAgreement.Id;
        }
    }
}
