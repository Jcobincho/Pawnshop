using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;

namespace Pawnshop.Application.ItemCategoriesApplication.Queries.GetAllItemCategories
{
    public sealed class GetAllItemCategoriesHandler : IRequestHandler<GetAllItemCategoriesQuery, GetAllItemCategoriesResponse>
    {
        private readonly IItemCategoriesQueryService _itemCategoriesQueryService;

        public GetAllItemCategoriesHandler(IItemCategoriesQueryService itemCategoriesQueryService)
        {
            _itemCategoriesQueryService = itemCategoriesQueryService;
        }

        public async Task<GetAllItemCategoriesResponse> Handle(GetAllItemCategoriesQuery request, CancellationToken cancellationToken)
        {
            var itemCategories = await _itemCategoriesQueryService.GetAllItemCategoriesAsDtoAsync(cancellationToken);

            return new GetAllItemCategoriesResponse
            {
                AllItemCategoriesList = itemCategories
            };
        }
    }
}
