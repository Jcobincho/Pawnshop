using MediatR;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Queries.GetItemsForPurchaseSaleTransaction
{
    public sealed class GetItemsForPurchaseSaleTransactionHandler : IRequestHandler<GetItemsForPurchaseSaleTransactionQuery, GetItemsForPurchaseSaleTransactionResponse>
    {
        private readonly IItemInPurchaseSaleTransactionQueryService _itemInPurchaseSaleTransactionQueryService;

        public GetItemsForPurchaseSaleTransactionHandler(IItemInPurchaseSaleTransactionQueryService itemInPurchaseSaleTransactionQueryService)
        {
            _itemInPurchaseSaleTransactionQueryService = itemInPurchaseSaleTransactionQueryService;
        }

        public async Task<GetItemsForPurchaseSaleTransactionResponse> Handle(GetItemsForPurchaseSaleTransactionQuery request, CancellationToken cancellationToken)
        {
            var itemsInPurchaseSaleTransactions = await _itemInPurchaseSaleTransactionQueryService.GetItemsForPurchaseSaleTransactionAsync(request.PurchaseSaleTransactionId, cancellationToken);

            return new GetItemsForPurchaseSaleTransactionResponse
            {
                ItemsForPurchaseSaleTransactionList = itemsInPurchaseSaleTransactions
            };
        }
    }
}
