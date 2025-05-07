using MediatR;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Responses;

namespace Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace
{
    public sealed class AddWorkplaceHandler : IRequestHandler<AddWorkplaceCommand, AddWorkplaceResponse>
    {
        private readonly IWorkplacesCommandService _workplacesCommandService;

        public AddWorkplaceHandler(IWorkplacesCommandService workplacesCommandService)
        {
            _workplacesCommandService = workplacesCommandService;
        }

        public async Task<AddWorkplaceResponse> Handle(AddWorkplaceCommand request, CancellationToken cancellationToken)
        {
            var workplaceId = await _workplacesCommandService.AddWorkplaceAsync(request, cancellationToken);

            return new AddWorkplaceResponse()
            {
                WorkplaceId = workplaceId,
            };
        }
    }
}
