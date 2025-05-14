using Pawnshop.Application.Base;

namespace Pawnshop.Application.ItemCategoriesApplication.Dto
{
    public class ItemCategoryDto : BaseDto
    {
        public Guid ItemCategoryId {get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
