using Pawnshop.Application.ItemCategoriesApplication.Dto;

namespace Pawnshop.Application.ItemCategoriesApplication.Responses
{
    public sealed class GetAllItemCategoriesResponse
    {
        public List<ItemCategoryDto> AllItemCategoriesList {get; set;}
    }
}
