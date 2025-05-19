using Pawnshop.Application.Base;
using Pawnshop.Domain.Enums;
using Pawnshop.Application.WorkplacesApplication.Dto;

namespace Pawnshop.Application.ItemHistoriesApplication.Dto
{
    public class ItemHistoryForItemDetailDto : BaseDto
    {
        public Guid ItemDetailId { get; set; }

        public ItemStatus ItemStatus { get; set; }
        public string Description { get; set; }

        public WorkplaceDto Workplace { get; set; }

        public DateTime DateFrom { get; set; }
    }
}
