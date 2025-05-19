using MediatR;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;

namespace Pawnshop.Application.ItemHistoriesApplication.Commands.AddItemHistory
{
    public sealed class AddItemHistoryHandler : IRequestHandler<AddItemHistoryCommand, AddItemCategoryResponse>
    {
        private readonly IItemHistoriesCommandService _itemHistoriesCommandService;

        public AddItemHistoryHandler(IItemHistoriesCommandService itemHistoriesCommandService)
        {
            _itemHistoriesCommandService = itemHistoriesCommandService;
        }

        public async Task<AddItemCategoryResponse> Handle(AddItemHistoryCommand request, CancellationToken cancellationToken)
        {
            var itemHistoryId = await _itemHistoriesCommandService.AddItemHistoryAsync(request, cancellationToken);

            return new AddItemCategoryResponse
            {
                CategoryId = itemHistoryId,
            };
        }
    }
}
