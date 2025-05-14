using Pawnshop.Application.ItemDetailsApplication.Dto;

namespace Pawnshop.Application.ItemDetailsApplication.Responses
{
    public sealed class GetAllItemDetailsResponse
    {
        public List<ItemDetailDto> AllItemDetailsList { get; set; }
    }
}
