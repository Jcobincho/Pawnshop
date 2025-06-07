using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument
{
    public sealed class UpdatePurchaseSaleTransactionDocumentHandler : IRequestHandler<UpdatePurchaseSaleTransactionDocumentCommand, UpdatePurchaseSaleTransactionDocumentResponse>
    {
        private readonly IPurchasesSaleTransactionCommandService _purchasesSaleTransactionCommandService;

        public UpdatePurchaseSaleTransactionDocumentHandler(IPurchasesSaleTransactionCommandService purchasesSaleTransactionCommandService)
        {
            _purchasesSaleTransactionCommandService = purchasesSaleTransactionCommandService;
        }

        public async Task<UpdatePurchaseSaleTransactionDocumentResponse> Handle(UpdatePurchaseSaleTransactionDocumentCommand request, CancellationToken cancellationToken)
        {
            await _purchasesSaleTransactionCommandService.UpdatePurchaseSaleTransactionAsync(request, cancellationToken);

            return new UpdatePurchaseSaleTransactionDocumentResponse();
        }
    }
}
