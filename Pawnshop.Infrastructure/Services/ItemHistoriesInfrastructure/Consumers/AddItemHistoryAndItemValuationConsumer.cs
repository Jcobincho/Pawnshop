using MassTransit;
using MediatR;
using Pawnshop.Application.ItemHistoriesApplication.Commands.AddItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Consumers.AddItemHistoryAndItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Consumers
{
    internal sealed class AddItemHistoryAndItemValuationConsumer : IConsumer<AddItemHistoryAndItemValuationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;

        public AddItemHistoryAndItemValuationConsumer(IMediator mediator, IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService)
        {
            _mediator = mediator;
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
        }

        public async Task Consume(ConsumeContext<AddItemHistoryAndItemValuationEvent> context)
        {
            try
            {
                var message = context.Message;

                var document = await _purchasesSaleTransactionQueryService.GetPurchaseSaleTransactionByIdAsync(message.PurchaseSaleTransactionId, new CancellationToken());

                if (document != null)
                {
                    var addItemHistoryCommand = new AddItemHistoryCommand
                    {
                        ItemDetailId = message.ItemDetailId,
                        Description = message.Description,
                        WorkplaceId = document.WorkplaceId,
                        DateFrom = DateTime.Now,
                        TransactionPrice = message.ItemPrice
                    };

                    if (document.TypeOfTransaction == TypeOfTransactionEnum.Purchase)
                    {
                        addItemHistoryCommand.ItemStatus = ItemStatus.PawnPurchased;
                    }
                    else if (document.TypeOfTransaction == TypeOfTransactionEnum.Sale)
                    {
                        addItemHistoryCommand.ItemStatus = ItemStatus.Sold;
                    }

                    var itemHistoryId = await _mediator.Send(addItemHistoryCommand);

                    if (message.AddItemValuation && itemHistoryId != null)
                    {
                        var addItemValuation = new AddItemValuationCommand
                        {
                            ItemHistoryId = itemHistoryId.CategoryId,
                            ValuationOnDate = DateTime.Now,
                            Price = message.ItemValuationPrice,
                            Justification = message.Justification,
                        };

                        await _mediator.Send(addItemValuation);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
