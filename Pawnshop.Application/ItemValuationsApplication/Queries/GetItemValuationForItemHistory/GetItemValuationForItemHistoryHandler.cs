using MediatR;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Responses;

namespace Pawnshop.Application.ItemValuationsApplication.Queries.GetItemValuationForItemHistory
{
    public sealed class GetItemValuationForItemHistoryHandler : IRequestHandler<GetItemValuationForItemHistoryQuery, GetItemValuationForItemHistoryResponse>
    {
        private readonly IItemValuationsQueryService _itemValuationsQueryService;

        public GetItemValuationForItemHistoryHandler(IItemValuationsQueryService itemValuationsQueryService)
        {
            _itemValuationsQueryService = itemValuationsQueryService;
        }

        public async Task<GetItemValuationForItemHistoryResponse> Handle(GetItemValuationForItemHistoryQuery request, CancellationToken cancellationToken)
        {
            var itemValuations = await _itemValuationsQueryService.GetItemValuationForItemHistoryAsync(request, cancellationToken);

            return new GetItemValuationForItemHistoryResponse
            {
                ItemValuationyList = itemValuations
            };
        }
    }
}
