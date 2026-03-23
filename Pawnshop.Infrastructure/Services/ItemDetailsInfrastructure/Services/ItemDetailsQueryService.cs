using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemDetailsApplication.Dto;
using Pawnshop.Application.ItemDetailsApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemDetailsInfrastructure.Services
{
    internal sealed class ItemDetailsQueryService : IItemDetailsQueryService
    {
        private readonly DbContext _dbContext;

        public ItemDetailsQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ItemDetail> GetItemDetailByIdAsync(Guid itemDetailId, CancellationToken cancellationToken)
        {
            var itemDetail = await _dbContext.ItemsDetail.FindAsync(itemDetailId, cancellationToken);

            if (itemDetail == null)
            {
                throw new NotFoundException("Item doesn't exist.");
            }

            return itemDetail;
        }

        public async Task<List<ItemDetailDto>> GetAllItemDetailsAsDtoAsync(CancellationToken cancellationToken)
        {
            var itemDetailsList = await _dbContext.ItemsDetail.Select(x => x.ItemDetailParseToDto()).ToListAsync();

            return itemDetailsList;
        }

        public async Task<bool> IsItemDetailExistsAsync(Guid itemDetailId, CancellationToken cancellationToken)
        {
            return await _dbContext.ItemsDetail.AnyAsync(x => x.Id == itemDetailId, cancellationToken);
        }
    }
}
