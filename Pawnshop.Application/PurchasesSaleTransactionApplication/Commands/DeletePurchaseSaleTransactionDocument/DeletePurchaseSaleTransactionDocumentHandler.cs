using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument
{
    public sealed class DeletePurchaseSaleTransactionDocumentHandler : IRequestHandler<DeletePurchaseSaleTransactionDocumentCommand, DeletePurchaseSaleTransactionDocumentResponse>
    {
        private readonly IPurchasesSaleTransactionCommandService _purchasesSaleTransactionCommandService;

        public DeletePurchaseSaleTransactionDocumentHandler(IPurchasesSaleTransactionCommandService purchasesSaleTransactionCommandService)
        {
            _purchasesSaleTransactionCommandService = purchasesSaleTransactionCommandService;
        }

        public async Task<DeletePurchaseSaleTransactionDocumentResponse> Handle(DeletePurchaseSaleTransactionDocumentCommand request, CancellationToken cancellationToken)
        {
            await _purchasesSaleTransactionCommandService.DeletePurchaseSaleTransactionAsync(request, cancellationToken);

            return new DeletePurchaseSaleTransactionDocumentResponse();
        }
    }
}
