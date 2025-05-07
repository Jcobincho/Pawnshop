using MediatR;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Responses;

namespace Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace
{
    public sealed class DeleteWorkplaceHandler : IRequestHandler<DeleteWorkplaceCommand, DeleteWorkplaceResponse>
    {
        private readonly IWorkplacesCommandService _workplacesCommandService;

        public DeleteWorkplaceHandler(IWorkplacesCommandService workplacesCommandService)
        {
            _workplacesCommandService = workplacesCommandService;
        }

        public async Task<DeleteWorkplaceResponse> Handle(DeleteWorkplaceCommand request, CancellationToken cancellationToken)
        {
            await _workplacesCommandService.DeleteWorkplaceAsync(request, cancellationToken);

            return new DeleteWorkplaceResponse();
        }
    }
}
