using MediatR;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Responses;

namespace Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace
{
    public sealed class UpdateWorkplaceHandler : IRequestHandler<UpdateWorkplaceCommand, UpdateWorkplaceResponse>
    {
        private readonly IWorkplacesCommandService _workplacesCommandService;

        public UpdateWorkplaceHandler(IWorkplacesCommandService workplacesCommandService)
        {
            _workplacesCommandService = workplacesCommandService;
        }

        public async Task<UpdateWorkplaceResponse> Handle(UpdateWorkplaceCommand request, CancellationToken cancellationToken)
        {
            await _workplacesCommandService.UpdateWorkplaceAsync(request, cancellationToken);

            return new UpdateWorkplaceResponse();
        }
    }
}
