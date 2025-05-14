using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteCategory
{
    public sealed class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
    {
        private readonly IItemCaterogoriesCommandService _caterogoriesCommandService;
        public DeleteCategoryHandler(IItemCaterogoriesCommandService caterogoriesCommandService)
        {
            _caterogoriesCommandService = caterogoriesCommandService;
        }
        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _caterogoriesCommandService.DeleteCategoryServiceAsync(request, cancellationToken);
            return new DeleteCategoryResponse();
        }
    }
}
