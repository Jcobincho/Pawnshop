using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemCategoriesApplication.Dto;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto
{
    public class ItemInPurchaseSaleTransactionDto : BaseDto
    {
        public Guid ItemInPurchaseSaleTransactionId { get; set; }
        public Guid ItemDetailId { get; set; }
        public string Name { get; set; }
        public ItemCategoryDto ItemCategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime AddedOn { get; set; }
        public string Comments { get; set; }
        public float Price { get; set; }
    }
}
