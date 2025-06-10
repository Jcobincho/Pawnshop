using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Item
{
    public class ItemHistory : BaseEntity
    {
        public Guid ItemDetailId { get; set; }
        public ItemDetail ItemDetail { get; set; }

        public ItemStatus ItemStatus { get; set; }
        public string Description { get; set; }

        public Guid WorkplaceId { get; set; }
        public Workplace Workplace { get; set; }

        public DateTime DateFrom { get; set; }
        public float TransactionPrice { get; set; }
    }
}
