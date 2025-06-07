using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemValuationsApplication.Dto.DtoExtension
{
    public static class ItemValuationDtoExtension
    {
        public static ItemValuationForItemHistoryDto ItemValuationForItemHistoryParseToDto(this ItemValuation itemValuation)
        {
            return new ItemValuationForItemHistoryDto
            {
                ItemHistoryId = itemValuation.ItemHistoryId,
                ValuationOnDate = itemValuation.ValuationOnDate,
                Price = itemValuation.Price,
                Justification = itemValuation.Justification,
                CreatedAt = itemValuation.CreatedAt,
                CreatedBy = itemValuation.CreatedBy,
                EditedAt = itemValuation.EditedAt,
                EditedBy = itemValuation.EditedBy
            };
        }
    }
}
