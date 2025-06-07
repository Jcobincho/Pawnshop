using MediatR;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Responses;

namespace Pawnshop.Application.ItemHistoriesApplication.Commands.DeleteItemHistory
{
    public sealed class DeleteItemHistoryHandler : IRequestHandler<DeleteItemHistoryCommand, DeleteItemHistoryResponse>
    {
        private readonly IItemHistoriesCommandService _itemHistoriesCommandService;

        public DeleteItemHistoryHandler(IItemHistoriesCommandService itemHistoriesCommandService)
        {
            _itemHistoriesCommandService = itemHistoriesCommandService;
        }

        public async Task<DeleteItemHistoryResponse> Handle(DeleteItemHistoryCommand request, CancellationToken cancellationToken)
        {
            await _itemHistoriesCommandService.DeleteItemHistoryAsync(request, cancellationToken);

            return new DeleteItemHistoryResponse();
        }
    }
}
