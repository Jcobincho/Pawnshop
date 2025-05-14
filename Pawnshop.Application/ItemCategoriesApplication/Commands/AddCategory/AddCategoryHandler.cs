using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.AddCategory
{
    public sealed class AddCategoryHandler : IRequestHandler<AddCategoryCommand, AddCategoryResponse>
    {
        private readonly IItemCaterogoriesCommandService _itemCaterogoriesCommandService;
        public AddCategoryHandler(IItemCaterogoriesCommandService itemCaterogoriesCommandService)
        {
            _itemCaterogoriesCommandService = itemCaterogoriesCommandService;
        }
        public async Task<AddCategoryResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var itemCategoryId = await _itemCaterogoriesCommandService.AddCategoryServiceAsync(request, cancellationToken);

            return new AddCategoryResponse()
            {
                CategoryId = itemCategoryId,
            };
        }
    }
}
