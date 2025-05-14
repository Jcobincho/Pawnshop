using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var itemCategories = await _itemCategoriesQueryService.GetAllItemCategoriesAsync(cancellationToken);
            return new GetAllItemCategoriesResponse
            {
                AllItemCategoriesList = itemCategories
            };
        }
    }
}
