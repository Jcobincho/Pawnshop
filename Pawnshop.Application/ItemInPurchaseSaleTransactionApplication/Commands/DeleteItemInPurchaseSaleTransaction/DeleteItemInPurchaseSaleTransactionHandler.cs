using MediatR;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.DeleteItemInPurchaseSaleTransaction
{
    public sealed class DeleteItemInPurchaseSaleTransactionHandler : IRequestHandler<DeleteItemInPurchaseSaleTransactionCommand, DeleteItemInPurchaseSaleTransactionResponse>
    {
        private readonly IItemInPurchaseSaleTransactionCommandService _itemInPurchaseSaleTransactionCommandService;

        public DeleteItemInPurchaseSaleTransactionHandler(IItemInPurchaseSaleTransactionCommandService itemInPurchaseSaleTransactionCommandService)
        {
            _itemInPurchaseSaleTransactionCommandService = itemInPurchaseSaleTransactionCommandService;
        }

        public async Task<DeleteItemInPurchaseSaleTransactionResponse> Handle(DeleteItemInPurchaseSaleTransactionCommand request, CancellationToken cancellationToken)
        {
            await _itemInPurchaseSaleTransactionCommandService.DeleteItemInPurchaseSaleTransactionAsync(request, cancellationToken);

            return new DeleteItemInPurchaseSaleTransactionResponse();
        }
    }
}
