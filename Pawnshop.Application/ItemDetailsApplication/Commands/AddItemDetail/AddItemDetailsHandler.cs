using MediatR;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Responses;

namespace Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail
{
    public sealed class AddItemDetailsHandler : IRequestHandler<AddItemDetailsCommand, AddItemDetailsResponse>
    {
        private readonly IItemDetailsCommandService _itemDetailsCommandService;

        public AddItemDetailsHandler(IItemDetailsCommandService itemDetailsCommandService)
        {
            _itemDetailsCommandService = itemDetailsCommandService;
        }

        public async Task<AddItemDetailsResponse> Handle(AddItemDetailsCommand request, CancellationToken cancellationToken)
        {
            var id = await _itemDetailsCommandService.AddItemDetailAsync(request, cancellationToken);

            return new AddItemDetailsResponse
            {
                Id = id
            };
        }
    }
}
