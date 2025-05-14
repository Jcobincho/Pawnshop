using MediatR;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Responses;

namespace Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail
{
    public sealed class DeleteItemDetailHandler : IRequestHandler<DeleteItemDetailCommand, DeleteItemDetailResponse>
    {
        private readonly IItemDetailsCommandService _itemDetailsCommandService;

        public DeleteItemDetailHandler(IItemDetailsCommandService itemDetailsCommandService)
        {
            _itemDetailsCommandService = itemDetailsCommandService;
        }

        public async Task<DeleteItemDetailResponse> Handle(DeleteItemDetailCommand request, CancellationToken cancellationToken)
        {
            await _itemDetailsCommandService.DeleteItemDetailAsync(request, cancellationToken);

            return new DeleteItemDetailResponse();
        }
    }
}
