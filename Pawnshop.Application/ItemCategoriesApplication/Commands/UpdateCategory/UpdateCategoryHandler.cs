using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateCategory
{
    public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        private readonly IItemCaterogoriesCommandService _itemCaterogoriesCommandService;
        public UpdateCategoryHandler(IItemCaterogoriesCommandService itemCaterogoriesCommandService)
        {
            _itemCaterogoriesCommandService = itemCaterogoriesCommandService;
        }
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await _itemCaterogoriesCommandService.UpdateCategoryServiceAsync(request, cancellationToken);
            return new UpdateCategoryResponse();
        }
    }
}
