using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteItemCategory
{
    public sealed class DeleteItemCategoryHandler : IRequestHandler<DeleteItemCategoryCommand, DeleteItemCategoryResponse>
    {
        private readonly IItemCaterogoriesCommandService _caterogoriesCommandService;
        public DeleteItemCategoryHandler(IItemCaterogoriesCommandService caterogoriesCommandService)
        {
            _caterogoriesCommandService = caterogoriesCommandService;
        }
        public async Task<DeleteItemCategoryResponse> Handle(DeleteItemCategoryCommand request, CancellationToken cancellationToken)
        {
            await _caterogoriesCommandService.DeleteItemCategoryServiceAsync(request, cancellationToken);
            return new DeleteItemCategoryResponse();
        }
    }
}
