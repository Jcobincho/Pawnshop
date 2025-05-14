using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateItemCategory
{
    public sealed class UpdateItemCategoryHandler : IRequestHandler<UpdateItemCategoryCommand, UpdateItemCategoryResponse>
    {
        private readonly IItemCaterogoriesCommandService _itemCaterogoriesCommandService;

        public UpdateItemCategoryHandler(IItemCaterogoriesCommandService itemCaterogoriesCommandService)
        {
            _itemCaterogoriesCommandService = itemCaterogoriesCommandService;
        }

        public async Task<UpdateItemCategoryResponse> Handle(UpdateItemCategoryCommand request, CancellationToken cancellationToken)
        {
            await _itemCaterogoriesCommandService.UpdateItemCategoryServiceAsync(request, cancellationToken);

            return new UpdateItemCategoryResponse();
        }
    }
}
