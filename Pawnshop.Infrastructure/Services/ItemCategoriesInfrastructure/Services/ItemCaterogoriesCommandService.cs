using Pawnshop.Application.ItemCategoriesApplication.Commands.AddItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.ItemCategoriesInfrastructure.Services
{
    internal sealed class ItemCaterogoriesCommandService : IItemCaterogoriesCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IItemCategoriesQueryService _itemCategoriesQueryService;

        public ItemCaterogoriesCommandService(DbContext dbContext, IItemCategoriesQueryService itemCategoriesQueryService)
        {
            _dbContext = dbContext;
            _itemCategoriesQueryService = itemCategoriesQueryService;
        }

        public async Task<Guid> AddItemCategoryServiceAsync(AddItemCategoryCommand command, CancellationToken cancellationToken)
        {
            ItemCategory newItemCategory = new ItemCategory()
            {
                Name = command.Name,
                Description = command.Description,
            };

            await _dbContext.AddAsync(newItemCategory, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return newItemCategory.Id;
        }

        public async Task UpdateItemCategoryServiceAsync(UpdateItemCategoryCommand command, CancellationToken cancellationToken)
        {
            var itemCategory = await _itemCategoriesQueryService.GetItemCategoryByIdAsync(command.ItemCategoryId, cancellationToken);

            itemCategory.Name = command.Name;
            itemCategory.Description = command.Description;

            _dbContext.ItemCategories.Update(itemCategory);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteItemCategoryServiceAsync(DeleteItemCategoryCommand command, CancellationToken cancellationToken)
        {
            var itemCategory = await _itemCategoriesQueryService.GetItemCategoryByIdAsync(command.CategoryId, cancellationToken);

            _dbContext.ItemCategories.Remove(itemCategory);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
