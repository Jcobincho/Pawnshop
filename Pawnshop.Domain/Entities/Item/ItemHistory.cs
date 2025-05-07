using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Item
{
    public class ItemHistory : BaseEntity
    {
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public ItemStatus ItemStatus { get; set; }
        public string Description { get; set; }

        public Guid WorkplaceId { get; set; }
        public Workplace Workplace { get; set; }

        public Guid UserId { get; set; }
        public Users User { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
