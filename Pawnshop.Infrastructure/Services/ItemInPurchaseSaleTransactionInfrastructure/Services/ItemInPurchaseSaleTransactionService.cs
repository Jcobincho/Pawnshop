using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Domain.Entities.Transactions;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemInPurchaseSaleTransactionInfrastructure.Services
{
    internal sealed class ItemInPurchaseSaleTransactionService : IItemInPurchaseSaleTransactionCommandService, IItemInPurchaseSaleTransactionQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;
        private readonly IItemDetailsQueryService _itemDetailsQueryService;

        public ItemInPurchaseSaleTransactionService(DbContext dbContext, IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService, IItemDetailsQueryService itemDetailsQueryService)
        {
            _dbContext = dbContext;
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
            _itemDetailsQueryService = itemDetailsQueryService;
        }

        public async Task<Guid> AddItemInPurchaseSaleTransactionAsync(AddItemInPurchaseSaleTransactionCommand command, CancellationToken cancellationToken)
        {
            await IsItemDetailAndPurchaseSaleTransactionExistAsync(command.PurchaseSaleTransactionId, command.ItemDetailId, cancellationToken);

            var itemInPurchaseSaleTransaction = new ItemInPurchaseSaleTransaction()
            {
                PurchaseSaleTransactionId = command.PurchaseSaleTransactionId,
                ItemDetailId = command.ItemDetailId,
                ItemPrice = command.ItemPrice,
            };

            await _dbContext.ItemsInPurchaseSaleTransaction.AddAsync(itemInPurchaseSaleTransaction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return itemInPurchaseSaleTransaction.Id;
        }

        private async Task IsItemDetailAndPurchaseSaleTransactionExistAsync(Guid purchaseSaleTransactionId, Guid ItemDetailId, CancellationToken cancellationToken)
        {
            var isPurchaseSaleTransactionExist = await _purchasesSaleTransactionQueryService.IsPurchaseSaleTransactionExistAsync(purchaseSaleTransactionId, cancellationToken);

            if (!isPurchaseSaleTransactionExist)
                throw new NotFoundException("Transaction document doesn't exist");

            var isItemDetailExist = await _itemDetailsQueryService.IsItemDetailExistsAsync(ItemDetailId, cancellationToken);
             
            if (!isItemDetailExist)
                throw new NotFoundException("Item doesn't exist");
        }
    }
}
