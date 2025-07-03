using Pawnshop.Application.ItemDetailsApplication.Interfaces;
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
    }
}
