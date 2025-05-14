using MediatR;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Responses;

namespace Pawnshop.Application.ItemDetailsApplication.Commands.UpdateItemDetail
{
    public sealed class UpdateItemDetailHandler : IRequestHandler<UpdateItemDetailCommand, UpdateItemDetailResponse>
    {
        private readonly IItemDetailsCommandService _itemDetailsCommandService;

        public UpdateItemDetailHandler(IItemDetailsCommandService itemDetailsCommandService)
        {
            _itemDetailsCommandService = itemDetailsCommandService;
        }

        public async Task<UpdateItemDetailResponse> Handle(UpdateItemDetailCommand request, CancellationToken cancellationToken)
        {
            await _itemDetailsCommandService.UpdateItemDetailAsync(request, cancellationToken);

            return new UpdateItemDetailResponse();
        }
    }
}
