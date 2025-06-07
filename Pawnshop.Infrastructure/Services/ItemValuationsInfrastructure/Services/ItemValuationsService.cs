using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.DeleteItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.UpdateItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Dto;
using Pawnshop.Application.ItemValuationsApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Queries.GetItemValuationForItemHistory;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemValuationsInfrastructure.Services
{
    internal sealed class ItemValuationsService : IItemValuationsCommandService, IItemValuationsQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IItemHistoriesQueryService _itemHistoriesQueryService;

        public ItemValuationsService(DbContext dbContext, IItemHistoriesQueryService itemHistoriesQueryService)
        {
            _dbContext = dbContext;
            _itemHistoriesQueryService = itemHistoriesQueryService;
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
            var itemValuation = await GetItemValuationByIdAsync(command.ItemValuationId, cancellationToken);

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
            var itemValuation = await GetItemValuationByIdAsync(command.ItemValuationId, cancellationToken);

            _dbContext.ItemsValuation.Remove(itemValuation);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ItemValuation> GetItemValuationByIdAsync(Guid itemValuationId, CancellationToken cancellationToken)
        {
            var itemValuation = await _dbContext.ItemsValuation.FindAsync(itemValuationId, cancellationToken);

            if (itemValuation == null)
                throw new NotFoundException("Item valuation doesn't exist.");

            return itemValuation;
        }

        public async Task<List<ItemValuationForItemHistoryDto>> GetItemValuationForItemHistoryAsync(GetItemValuationForItemHistoryQuery query, CancellationToken cancellationToken)
        {
            var itemValuation = await _dbContext.ItemsValuation
                .Where(valuation => valuation.ItemHistoryId == query.ItemHistoryId)
                .OrderByDescending(valuation => valuation.ValuationOnDate)
                .Select(valuation => valuation.ItemValuationForItemHistoryParseToDto())
                .ToListAsync(cancellationToken);

            if (itemValuation == null)
                throw new NotFoundException("Item valuations doesn't exist.");

            return itemValuation;
        }
    }
}
