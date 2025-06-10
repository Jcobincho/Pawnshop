using MediatR;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction
{
    public sealed class AddItemInPurchaseSaleTransactionHandler : IRequestHandler<AddItemInPurchaseSaleTransactionCommand, AddItemInPurchaseSaleTransactionResponse>
    {
        private readonly IItemInPurchaseSaleTransactionCommandService _itemInPurchaseSaleTransactionCommandService;

        public AddItemInPurchaseSaleTransactionHandler(IItemInPurchaseSaleTransactionCommandService itemInPurchaseSaleTransactionCommandService)
        {
            _itemInPurchaseSaleTransactionCommandService = itemInPurchaseSaleTransactionCommandService;
        }

        public async Task<AddItemInPurchaseSaleTransactionResponse> Handle(AddItemInPurchaseSaleTransactionCommand request, CancellationToken cancellationToken)
        {
            var itemInPurchaseSaleTransactionId = await _itemInPurchaseSaleTransactionCommandService.AddItemInPurchaseSaleTransactionAsync(request, cancellationToken);

            return new AddItemInPurchaseSaleTransactionResponse
            {
                Id = itemInPurchaseSaleTransactionId
            };
        }
    }
}
