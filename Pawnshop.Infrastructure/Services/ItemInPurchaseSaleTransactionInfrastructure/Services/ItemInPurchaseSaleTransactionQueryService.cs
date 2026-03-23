using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemDetailsApplication.Dto;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Domain.Entities.Transactions;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemInPurchaseSaleTransactionInfrastructure.Services
{
    internal sealed class ItemInPurchaseSaleTransactionQueryService : IItemInPurchaseSaleTransactionQueryService
    {
        private readonly DbContext _dbContext;

        public ItemInPurchaseSaleTransactionQueryService(DbContext dbContext, IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService, IItemDetailsQueryService itemDetailsQueryService)
        {
            _dbContext = dbContext;
        }

        public async Task<ItemInPurchaseSaleTransaction> GetItemInPurchaseSaleTransactionAsync(Guid ItemInPurchaseSaleTransactionId, CancellationToken cancellationToken)
        {
            var itemInPurchaseSaleTransaction = await _dbContext.ItemsInPurchaseSaleTransaction.FindAsync(ItemInPurchaseSaleTransactionId, cancellationToken);

            if (itemInPurchaseSaleTransaction == null)
                throw new NotFoundException("Item in transaction doesn't exist.");

            return itemInPurchaseSaleTransaction;
        }

        public async Task<List<ItemInPurchaseSaleTransactionDto>> GetItemsForPurchaseSaleTransactionAsync(Guid purchaseSaleTransactionId, CancellationToken cancellationToken)
        {
            var items = await _dbContext.ItemsInPurchaseSaleTransaction
                                        .Where(x => x.PurchaseSaleTransactionId == purchaseSaleTransactionId)
                                        .Include(x => x.ItemDetail)
                                            .ThenInclude(x => x.ItemCategory)
                                        .Select(x => x.ItemInPurchaseSaleTransactionParseToDto())
                                        .ToListAsync(cancellationToken);

            return items;
        }
    }
}
