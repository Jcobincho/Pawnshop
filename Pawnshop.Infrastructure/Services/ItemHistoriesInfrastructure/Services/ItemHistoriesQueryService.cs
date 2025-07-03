using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemHistoriesApplication.Dto;
using Pawnshop.Application.ItemHistoriesApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Queries.GetItemHistoriesForItemDetail;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Services
{
    internal sealed class ItemHistoriesQueryService : IItemHistoriesQueryService
    {
        private readonly DbContext _dbContext;

        public ItemHistoriesQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ItemHistory> GetItemHistoryByIdAsync(Guid itemHistoryId, CancellationToken cancellationToken)
        {
            var itemHistory = await _dbContext.ItemHistories.FindAsync(itemHistoryId, cancellationToken);

            if (itemHistory == null)
                throw new NotFoundException("Item history doesn't exist.");

            return itemHistory;
        }

        public async Task<List<ItemHistoryForItemDetailDto>> GetItemHistoryForItemDetailAsync(GetItemHistoriesForItemDetailQuery query, CancellationToken cancellationToken)
        {
            var itemHistories = await _dbContext.ItemHistories
                .Where(history => history.ItemDetailId == query.ItemDetailId)
                .Include(history => history.Workplace)
                .OrderByDescending(history => history.DateFrom)
                .Select(history => history.ItemHistoryForItemDetailParseToDto())
                .ToListAsync(cancellationToken);

            if (itemHistories == null)
                throw new NotFoundException("Item history doesn't exist.");

            return itemHistories;
        }

        public async Task<bool> IsItemHistoryExistAsync(Guid itemHistoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.ItemHistories.AnyAsync(x => x.Id == itemHistoryId, cancellationToken);
        }
    }
}
