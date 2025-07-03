using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.DeleteItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.UpdateItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemValuationsInfrastructure.Services
{
    internal sealed class ItemValuationsCommandService : IItemValuationsCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IItemHistoriesQueryService _itemHistoriesQueryService;
        private readonly IItemValuationsQueryService _itemValuationsQueryService;

        public ItemValuationsCommandService(DbContext dbContext, IItemHistoriesQueryService itemHistoriesQueryService, IItemValuationsQueryService itemValuationsQueryService)
        {
            _dbContext = dbContext;
            _itemHistoriesQueryService = itemHistoriesQueryService;
            _itemValuationsQueryService = itemValuationsQueryService;
        }

        public async Task<Guid> AddItemValuationAsync(AddItemValuationCommand command, CancellationToken cancellationToken)
        {
            var isItemHistoryExist = await _itemHistoriesQueryService.IsItemHistoryExistAsync(command.ItemHistoryId, cancellationToken);

            if (!isItemHistoryExist)
                throw new NotFoundException("Item history doesn't exist.");

            var newItemValuation = new ItemValuation
            {
                ItemHistoryId = command.ItemHistoryId,
                ValuationOnDate = command.ValuationOnDate,
                Price = command.Price,
                Justification = command.Justification
            };

            await _dbContext.ItemsValuation.AddAsync(newItemValuation, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newItemValuation.Id;
        }

        public async Task UpdateItemValuationAsync(UpdateItemValuationCommand command, CancellationToken cancellationToken)
        {
            var itemValuation = await _itemValuationsQueryService.GetItemValuationByIdAsync(command.ItemValuationId, cancellationToken);

            var isItemHistoryExist = await _itemHistoriesQueryService.IsItemHistoryExistAsync(command.ItemHistoryId, cancellationToken);

            if (!isItemHistoryExist)
                throw new NotFoundException("Item history doesn't exist.");

            itemValuation.ItemHistoryId = command.ItemHistoryId;
            itemValuation.ValuationOnDate = command.ValuationOnDate;
            itemValuation.Price = command.Price;
            itemValuation.Justification = command.Justification;

            _dbContext.ItemsValuation.Update(itemValuation);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteItemValuationAsync(DeleteItemValuationCommand command, CancellationToken cancellationToken)
        {
            var itemValuation = await _itemValuationsQueryService.GetItemValuationByIdAsync(command.ItemValuationId, cancellationToken);

            _dbContext.ItemsValuation.Remove(itemValuation);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
