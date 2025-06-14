using MediatR;
using Pawnshop.Application.ItemHistoriesApplication.Consumers.AddItemHistoryAndItemValuation;
using Pawnshop.Application.ItemHistoriesApplication.Producers;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction
{
    public sealed class AddItemInPurchaseSaleTransactionHandler : IRequestHandler<AddItemInPurchaseSaleTransactionCommand, AddItemInPurchaseSaleTransactionResponse>
    {
        private readonly IItemInPurchaseSaleTransactionCommandService _itemInPurchaseSaleTransactionCommandService;
        private readonly IItemHistoryEventPublisher _itemHistoryEventPublisher;

        public AddItemInPurchaseSaleTransactionHandler(IItemInPurchaseSaleTransactionCommandService itemInPurchaseSaleTransactionCommandService, IItemHistoryEventPublisher itemHistoryEventPublisher)
        {
            _itemInPurchaseSaleTransactionCommandService = itemInPurchaseSaleTransactionCommandService;
            _itemHistoryEventPublisher = itemHistoryEventPublisher;
        }

        public async Task<AddItemInPurchaseSaleTransactionResponse> Handle(AddItemInPurchaseSaleTransactionCommand request, CancellationToken cancellationToken)
        {
            var itemInPurchaseSaleTransactionId = await _itemInPurchaseSaleTransactionCommandService.AddItemInPurchaseSaleTransactionAsync(request, cancellationToken);

            if(request.AddItemHistory)
            {
                var addItemHistoryEvent = new AddItemHistoryAndItemValuationEvent
                (
                    request.PurchaseSaleTransactionId,
                    request.ItemDetailId,
                    request.ItemPrice,
                    request.Description,
                    request.AddItemValuation,
                    request.ItemValuationPrice,
                    request.Justification
                );

                await _itemHistoryEventPublisher.AddItemHistoryAndItemValuationPublishAsync(addItemHistoryEvent, cancellationToken);
            }

            return new AddItemInPurchaseSaleTransactionResponse
            {
                Id = itemInPurchaseSaleTransactionId
            };
        }
    }
}
