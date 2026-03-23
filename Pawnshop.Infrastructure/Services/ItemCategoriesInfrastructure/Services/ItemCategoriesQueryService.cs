using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemCategoriesApplication.Dto;
using Pawnshop.Application.ItemCategoriesApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemCategoriesInfrastructure.Services
{
    internal sealed class ItemCategoriesQueryService : IItemCategoriesQueryService
    {
        private readonly DbContext _dbContext;

        public ItemCategoriesQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ItemCategory> GetItemCategoryByIdAsync(Guid itemCategoryId, CancellationToken cancellationToken)
        {
            var itemCategory = await _dbContext.ItemCategories.FindAsync(itemCategoryId, cancellationToken);
            if (itemCategory == null)
            {
                throw new NotFoundException("Item Category doesn't exist");
            }

            return itemCategory;
        }
        public async Task<List<ItemCategoryDto>> GetAllItemCategoriesAsDtoAsync(CancellationToken cancellationToken)
        {
            var itemCategories = await _dbContext.ItemCategories.Select(x => x.ItemCategoryParseToDto()).ToListAsync(cancellationToken);
            return itemCategories;
        }

        public async Task<bool> CategoryExistsAsync(Guid itemCategoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.ItemCategories.AnyAsync(x => x.Id == itemCategoryId, cancellationToken);
        }
    }
}
