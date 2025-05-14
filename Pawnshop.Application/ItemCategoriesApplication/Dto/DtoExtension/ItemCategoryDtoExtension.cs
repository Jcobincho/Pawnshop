using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemCategoriesApplication.Dto.DtoExtension
{
    public static class ItemCategoryDtoExtension
    {
        public static ItemCategoryDto ItemCategoryParseToDto(this ItemCategory itemCategory)
        {
            return new ItemCategoryDto
            {
                ItemCategoryId = itemCategory.Id,
                Name = itemCategory.Name,
                Description = itemCategory.Description,
                CreatedAt = itemCategory.CreatedAt,
                CreatedBy = itemCategory.CreatedBy,
                EditedAt = itemCategory.EditedAt,
                EditedBy = itemCategory.EditedBy
            };
        }
    }
}
