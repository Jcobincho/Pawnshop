using Pawnshop.Application.ItemCategoriesApplication.Dto;
using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemCategoriesApplication.Interfaces
{
    public interface IItemCategoriesQueryService
    {
        Task<ItemCategory> GetItemCategoryByIdAsync(Guid itemCategoryId, CancellationToken cancellationToken);
        Task<List<ItemCategoryDto>> GetAllItemCategoriesAsDtoAsync(CancellationToken cancellationToken);
        Task<bool> CategoryExistsAsync(Guid itemCategoryId, CancellationToken cancellationToken);
    }
}
