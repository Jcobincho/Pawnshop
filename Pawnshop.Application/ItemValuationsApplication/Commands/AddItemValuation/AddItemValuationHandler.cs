using MediatR;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Responses;

namespace Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation
{
    public sealed class AddItemValuationHandler : IRequestHandler<AddItemValuationCommand, AddItemValuationResponse>
    {
        private readonly IItemValuationsCommandService _itemValuationsCommandService;

        public AddItemValuationHandler(IItemValuationsCommandService itemValuationsCommandService)
        {
            _itemValuationsCommandService = itemValuationsCommandService;
        }

        public async Task<AddItemValuationResponse> Handle(AddItemValuationCommand request, CancellationToken cancellationToken)
        {
            var itemValuationId = await _itemValuationsCommandService.AddItemValuationAsync(request, cancellationToken);

            return new AddItemValuationResponse
            {
                Id = itemValuationId
            };
        }
    }
}
