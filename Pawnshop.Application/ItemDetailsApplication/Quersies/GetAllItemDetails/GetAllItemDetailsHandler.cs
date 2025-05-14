using MediatR;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Responses;

namespace Pawnshop.Application.ItemDetailsApplication.Quersies.GetAllItemDetails
{
    public sealed class GetAllItemDetailsHandler : IRequestHandler<GetAllItemDetailsQuery, GetAllItemDetailsResponse>
    {
        private readonly IItemDetailsQueryService _itemDetailsQueryService;

        public GetAllItemDetailsHandler(IItemDetailsQueryService itemDetailsQueryService)
        {
            _itemDetailsQueryService = itemDetailsQueryService;
        }

        public async Task<GetAllItemDetailsResponse> Handle(GetAllItemDetailsQuery request, CancellationToken cancellationToken)
        {
            var itemDetailsList = await _itemDetailsQueryService.GetAllItemDetailsAsDtoAsync(cancellationToken);

            return new GetAllItemDetailsResponse
            {
                AllItemDetailsList = itemDetailsList,
            };
        }
    }
}
