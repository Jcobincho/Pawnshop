namespace Pawnshop.Domain.Entities.Item
{
    public class ItemValuation : BaseEntity
    {
        public Guid ItemHistoryId { get; set; }
        public ItemHistory ItemHistory { get; set; }
        public DateTime ValuationOnDate { get; set; }
        public float Price { get; set; }
        public string Justification { get; set; } = string.Empty;
    }
}
