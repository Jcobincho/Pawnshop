using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.DeleteItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Domain.Entities.Transactions;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemInPurchaseSaleTransactionInfrastructure.Services
{
    internal sealed class ItemInPurchaseSaleTransactionCommandService : IItemInPurchaseSaleTransactionCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IItemInPurchaseSaleTransactionQueryService _itemInPurchaseSaleTransactionQueryService;
        private readonly IItemDetailsQueryService _itemDetailsQueryService;
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;

        public ItemInPurchaseSaleTransactionCommandService(DbContext dbContext, IItemInPurchaseSaleTransactionQueryService itemInPurchaseSaleTransactionQueryService, IItemDetailsQueryService itemDetailsQueryService, IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService)
        {
            _dbContext = dbContext;
            _itemInPurchaseSaleTransactionQueryService = itemInPurchaseSaleTransactionQueryService;
            _itemDetailsQueryService = itemDetailsQueryService;
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
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

        public async Task DeleteItemInPurchaseSaleTransactionAsync(DeleteItemInPurchaseSaleTransactionCommand command, CancellationToken cancellationToken)
        {
            var itemInTransaction = await _itemInPurchaseSaleTransactionQueryService.GetItemInPurchaseSaleTransactionAsync(command.ItemInPurchaseSaleTransactionId, cancellationToken);

            _dbContext.ItemsInPurchaseSaleTransaction.Remove(itemInTransaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
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
