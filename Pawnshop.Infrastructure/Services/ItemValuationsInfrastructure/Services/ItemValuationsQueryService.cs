using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemValuationsApplication.Dto;
using Pawnshop.Application.ItemValuationsApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Queries.GetItemValuationForItemHistory;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemValuationsInfrastructure.Services
{
    internal sealed class ItemValuationsQueryService : IItemValuationsQueryService
    {
        private readonly DbContext _dbContext;

        public ItemValuationsQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
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
