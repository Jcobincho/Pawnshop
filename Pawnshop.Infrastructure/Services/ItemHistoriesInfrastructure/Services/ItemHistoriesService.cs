using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Commands.AddItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Commands.DeleteItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Commands.UpdateItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Services
{
    internal sealed class ItemHistoriesService : IItemHistoriesCommandService, IItemHistoriesQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IItemDetailsQueryService _itemDetailsQueryService;
        private readonly IWorkplacesQueryService _workplacesQueryService;

        public ItemHistoriesService(DbContext dbContext, IItemDetailsQueryService itemDetailsQueryService, IWorkplacesQueryService workplacesQueryService)
        {
            _dbContext = dbContext;
            _itemDetailsQueryService = itemDetailsQueryService;
            _workplacesQueryService = workplacesQueryService;
        }

        public async Task<Guid> AddItemHistoryAsync(AddItemHistoryCommand command, CancellationToken cancellationToken)
        {
            await CheckWrokplaceAndItemDetailExistsAsync(command.ItemDetailId, command.WorkplaceId, cancellationToken);

            var newItemHistory = new ItemHistory
            {
                ItemDetailId = command.ItemDetailId,
                ItemStatus = command.ItemStatus,
                Description = command.Description,
                WorkplaceId = command.WorkplaceId,
                DateFrom = command.DateFrom,
            };

            await _dbContext.ItemHistories.AddAsync(newItemHistory, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newItemHistory.Id;
        }

        public async Task UpdateItemHistoryAsync(UpdateItemHistoryCommand command, CancellationToken cancellationToken)
        {
            await CheckWrokplaceAndItemDetailExistsAsync(command.ItemDetailId, command.WorkplaceId, cancellationToken);

            var itemHistory = await GetItemHistoryByIdAsync(command.ItemHistoryId, cancellationToken);

            itemHistory.ItemDetailId = command.ItemDetailId;
            itemHistory.ItemStatus = command.ItemStatus;
            itemHistory.Description = command.Description;
            itemHistory.WorkplaceId = command.WorkplaceId;
            itemHistory.DateFrom = command.DateFrom;

            _dbContext.ItemHistories.Update(itemHistory);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteItemHistoryAsync(DeleteItemHistoryCommand command, CancellationToken cancellationToken)
        {
            var itemHistory = await GetItemHistoryByIdAsync(command.ItemHistoryId, cancellationToken);

            _dbContext.ItemHistories.Remove(itemHistory);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ItemHistory> GetItemHistoryByIdAsync(Guid itemHistoryId, CancellationToken cancellationToken)
        {
            var itemHistory = await _dbContext.ItemHistories.FindAsync(itemHistoryId, cancellationToken);

            if (itemHistory == null)
                throw new NotFoundException("Item history doesn't exist.");

            return itemHistory;
        }

        private async Task CheckWrokplaceAndItemDetailExistsAsync(Guid itemDetailId, Guid workplaceId, CancellationToken cancellationToken)
        {
            var isItemDetailExit = await _itemDetailsQueryService.ItemDetailExistsAsync(itemDetailId, cancellationToken);

            if (!isItemDetailExit)
                throw new NotFoundException("Item doesn't exist.");

            var isWorkplaceExist = await _workplacesQueryService.WorkplaceExistsAsync(workplaceId, cancellationToken);

            if(!isWorkplaceExist)
                throw new NotFoundException("Workplace doesn't exist.");
        }
    }
}
