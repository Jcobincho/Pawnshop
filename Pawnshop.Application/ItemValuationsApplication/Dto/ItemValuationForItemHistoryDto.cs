using Pawnshop.Application.Base;

namespace Pawnshop.Application.ItemValuationsApplication.Dto
{
    public class ItemValuationForItemHistoryDto : BaseDto
    {
        public Guid ItemHistoryId { get; set; }
        public DateTime ValuationOnDate { get; set; }
        public float Price { get; set; }
        public string Justification { get; set; } = string.Empty;
    }
}
