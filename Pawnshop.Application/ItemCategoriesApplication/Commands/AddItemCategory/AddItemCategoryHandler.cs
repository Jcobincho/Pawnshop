using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemCategoriesApplication.Responses;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.AddItemCategory
{
    public sealed class AddItemCategoryHandler : IRequestHandler<AddItemCategoryCommand, AddItemCategoryResponse>
    {
        private readonly IItemCaterogoriesCommandService _itemCaterogoriesCommandService;

        public AddItemCategoryHandler(IItemCaterogoriesCommandService itemCaterogoriesCommandService)
        {
            _itemCaterogoriesCommandService = itemCaterogoriesCommandService;
        }

        public async Task<AddItemCategoryResponse> Handle(AddItemCategoryCommand request, CancellationToken cancellationToken)
        {
            var itemCategoryId = await _itemCaterogoriesCommandService.AddItemCategoryServiceAsync(request, cancellationToken);

            return new AddItemCategoryResponse()
            {
                CategoryId = itemCategoryId,
            };
        }
    }
}
