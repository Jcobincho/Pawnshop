using MediatR;
using Pawnshop.Application.ItemValuationsApplication.Interfaces;
using Pawnshop.Application.ItemValuationsApplication.Responses;

namespace Pawnshop.Application.ItemValuationsApplication.Commands.UpdateItemValuation
{
    public sealed class UpdateItemValuationHandler : IRequestHandler<UpdateItemValuationCommand, UpdateItemValuationResponse>
    {
        private readonly IItemValuationsCommandService _itemValuationsCommandService;

        public UpdateItemValuationHandler(IItemValuationsCommandService itemValuationsCommandService)
        {
            _itemValuationsCommandService = itemValuationsCommandService;
        }

        public async Task<UpdateItemValuationResponse> Handle(UpdateItemValuationCommand request, CancellationToken cancellationToken)
        {
            await _itemValuationsCommandService.UpdateItemValuationAsync(request, cancellationToken);

            return new UpdateItemValuationResponse();
        }
    }
}
