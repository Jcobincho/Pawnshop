using Pawnshop.Application.ItemHistoriesApplication.Dto;

namespace Pawnshop.Application.ItemHistoriesApplication.Responses
{
    public sealed class GetItemHistoriesForItemDetailResponse
    {
        public List<ItemHistoryForItemDetailDto> ItemHistoryList { get; set; }
    }
}
