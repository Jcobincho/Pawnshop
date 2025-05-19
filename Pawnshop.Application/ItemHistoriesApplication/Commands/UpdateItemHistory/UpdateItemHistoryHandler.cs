using MediatR;
using Pawnshop.Application.ItemHistoriesApplication.Interfaces;
using Pawnshop.Application.ItemHistoriesApplication.Responses;

namespace Pawnshop.Application.ItemHistoriesApplication.Commands.UpdateItemHistory
{
    public sealed class UpdateItemHistoryHandler : IRequestHandler<UpdateItemHistoryCommand, UpdateItemHistoryResponse>
    {
        private readonly IItemHistoriesCommandService _itemHistoriesCommandService;

        public UpdateItemHistoryHandler(IItemHistoriesCommandService itemHistoriesCommandService)
        {
            _itemHistoriesCommandService = itemHistoriesCommandService;
        }

        public async Task<UpdateItemHistoryResponse> Handle(UpdateItemHistoryCommand request, CancellationToken cancellationToken)
        {
            await _itemHistoriesCommandService.UpdateItemHistoryAsync(request, cancellationToken);

            return new UpdateItemHistoryResponse();
        }
    }
}
