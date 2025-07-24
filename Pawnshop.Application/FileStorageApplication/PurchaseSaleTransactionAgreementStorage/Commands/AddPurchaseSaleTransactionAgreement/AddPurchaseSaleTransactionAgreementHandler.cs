using MediatR;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Interfaces;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Responses;

namespace Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Commands.AddPurchaseSaleTransactionAgreement
{
    public sealed class AddPurchaseSaleTransactionAgreementHandler : IRequestHandler<AddPurchaseSaleTransactionAgreementCommand, AddPurchaseSaleTransactionAgreementResponse>
    {
        private readonly IPurchaseSaleTransactionAgreementCommandService _purchaseSaleTransactionAgreementCommandService;

        public AddPurchaseSaleTransactionAgreementHandler(IPurchaseSaleTransactionAgreementCommandService purchaseSaleTransactionAgreementCommandService)
        {
            _purchaseSaleTransactionAgreementCommandService = purchaseSaleTransactionAgreementCommandService;
        }

        public async Task<AddPurchaseSaleTransactionAgreementResponse> Handle(AddPurchaseSaleTransactionAgreementCommand request, CancellationToken cancellationToken)
        {
            var agreementId = await _purchaseSaleTransactionAgreementCommandService.AddPurchaseSaleTransactionAgreementAsync(request, cancellationToken);

            return new AddPurchaseSaleTransactionAgreementResponse
            {
                AddPurchaseSaleTransactionAgreementId = agreementId,
            };
        }
    }
}
