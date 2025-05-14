namespace Pawnshop.Domain.Entities.Item
{
    public class ItemDetail : BaseEntity
    {
        public string Name { get; set; }

        public Guid ItemCategoryId { get; set; }
        public ItemCategory ItemCategory { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime AddedOn { get; set; }
        public string Comments { get; set; }
    }
}
