using MediatR;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Responses;

namespace Pawnshop.Application.ItemValuationsApplication.Commands.DeleteItemValuation
{
    public sealed class DeleteItemValuationHandler : IRequestHandler<DeleteItemValuationCommand, DeleteItemValuationResponse>
    {
        private readonly IItemValuationsCommandService _itemValuationsCommandService;

        public DeleteItemValuationHandler(IItemValuationsCommandService itemValuationsCommandService)
        {
            _itemValuationsCommandService = itemValuationsCommandService;
        }

        public async Task<DeleteItemValuationResponse> Handle(DeleteItemValuationCommand request, CancellationToken cancellationToken)
        {
            await _itemValuationsCommandService.DeleteItemValuationAsync(request, cancellationToken);

            return new DeleteItemValuationResponse();
        }
    }
}
