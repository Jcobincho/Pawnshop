using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemCategoriesApplication.Dto;

namespace Pawnshop.Application.ItemDetailsApplication.Dto
{
    public class ItemDetailDto : BaseDto
    {
        public Guid ItemDetailId { get; set; }
        public string Name { get; set; }
        public ItemCategoryDto ItemCategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime AddedOn { get; set; }
        public string Comments { get; set; }
    }
}
