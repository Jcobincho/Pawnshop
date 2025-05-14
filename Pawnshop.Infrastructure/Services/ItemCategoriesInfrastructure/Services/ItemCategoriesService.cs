using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemCategoriesApplication.Commands.AddItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Dto;
using Pawnshop.Application.ItemCategoriesApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemCategoriesInfrastructure.Services
{
    internal sealed class ItemCategoriesService : IItemCaterogoriesCommandService, IItemCategoriesQueryService
    {
        private readonly DbContext _dbContext;

        public ItemCategoriesService(DbContext dbContext)
        {
            _dbContext = dbContext;
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
            var itemCategory = await GetItemCategoryByIdAsync(command.ItemCategoryId, cancellationToken);

            itemCategory.Name = command.Name;
            itemCategory.Description = command.Description;

            _dbContext.ItemCategories.Update(itemCategory);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteItemCategoryServiceAsync(DeleteItemCategoryCommand command, CancellationToken cancellationToken)
        {
            var itemCategory = await GetItemCategoryByIdAsync(command.CategoryId, cancellationToken);

            _dbContext.ItemCategories.Remove(itemCategory);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ItemCategory> GetItemCategoryByIdAsync(Guid itemCategoryId, CancellationToken cancellationToken)
        {
            var itemCategory = await _dbContext.ItemCategories.FindAsync(itemCategoryId, cancellationToken);
            if(itemCategory == null)
            {
                throw new NotFoundException("Item Category doesn't exist");
            }

            return itemCategory;
        }
        public async Task<List<ItemCategoryDto>> GetAllItemCategoriesAsync(CancellationToken cancellationToken)
        {
            var itemCategories = await _dbContext.ItemCategories.Select(x => x.ItemCategoryParseToDto()).ToListAsync(cancellationToken);
            return itemCategories;
        }

    }
}
