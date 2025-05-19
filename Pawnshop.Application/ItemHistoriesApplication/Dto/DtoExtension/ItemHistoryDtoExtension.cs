using Pawnshop.Application.WorkplacesApplication.Dto.DtoExtension;
using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemHistoriesApplication.Dto.DtoExtension
{
    public static class ItemHistoryDtoExtension
    {
        public static ItemHistoryForItemDetailDto ItemHistoryForItemDetailParseToDto(this ItemHistory itemHistory)
        {
            return new ItemHistoryForItemDetailDto
            {
                ItemDetailId = itemHistory.ItemDetailId,
                ItemStatus = itemHistory.ItemStatus,
                Description = itemHistory.Description,
                Workplace = itemHistory.Workplace.WorkplaceParseToDto(),
                DateFrom = itemHistory.DateFrom,
                CreatedAt = itemHistory.CreatedAt,
                CreatedBy = itemHistory.CreatedBy,
                EditedAt = itemHistory.EditedAt,
                EditedBy = itemHistory.EditedBy
            };
        }
    }
}
