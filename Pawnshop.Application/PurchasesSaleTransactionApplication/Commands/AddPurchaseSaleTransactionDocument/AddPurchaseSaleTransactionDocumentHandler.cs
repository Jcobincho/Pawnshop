using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument
{
    public sealed class AddPurchaseSaleTransactionDocumentHandler : IRequestHandler<AddPurchaseSaleTransactionDocumentCommand, AddPurchaseSaleTransactionDocumentResponse>
    {
        private readonly IPurchasesSaleTransactionCommandService _purchasesSaleTransactionCommandService;

        public AddPurchaseSaleTransactionDocumentHandler(IPurchasesSaleTransactionCommandService purchasesSaleTransactionCommandService)
        {
            _purchasesSaleTransactionCommandService = purchasesSaleTransactionCommandService;
        }

        public async Task<AddPurchaseSaleTransactionDocumentResponse> Handle(AddPurchaseSaleTransactionDocumentCommand request, CancellationToken cancellationToken)
        {
            var id = await _purchasesSaleTransactionCommandService.AddPurchaseSaleTransactionAsync(request, cancellationToken);

            return new AddPurchaseSaleTransactionDocumentResponse
            {
                Id = id
            };
        }
    }
}
