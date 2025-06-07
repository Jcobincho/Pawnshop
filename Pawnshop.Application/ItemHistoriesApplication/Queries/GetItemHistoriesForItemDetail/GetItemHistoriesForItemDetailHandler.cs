using MediatR;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Responses;

namespace Pawnshop.Application.ItemHistoriesApplication.Queries.GetItemHistoriesForItemDetail
{
    public sealed class GetItemHistoriesForItemDetailHandler : IRequestHandler<GetItemHistoriesForItemDetailQuery, GetItemHistoriesForItemDetailResponse>
    {
        private readonly IItemHistoriesQueryService _itemHistoriesQueryService;

        public GetItemHistoriesForItemDetailHandler(IItemHistoriesQueryService itemHistoriesQueryService)
        {
            _itemHistoriesQueryService = itemHistoriesQueryService;
        }

        public async Task<GetItemHistoriesForItemDetailResponse> Handle(GetItemHistoriesForItemDetailQuery request, CancellationToken cancellationToken)
        {
            var itemHistoryForItemDetailList = await _itemHistoriesQueryService.GetItemHistoryForItemDetailAsync(request, cancellationToken);

            return new GetItemHistoriesForItemDetailResponse
            {
                ItemHistoryList = itemHistoryForItemDetailList,
            };
        }
    }
}
