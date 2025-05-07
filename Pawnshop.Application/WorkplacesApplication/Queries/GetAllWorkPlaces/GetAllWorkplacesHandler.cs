using MediatR;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Responses;

namespace Pawnshop.Application.WorkplacesApplication.Queries.GetAllWorkPlaces
{
    public sealed class GetAllWorkplacesHandler : IRequestHandler<GetAllWorkplacesQuery, GetAllWorkplacesResponse>
    {
        private readonly IWorkplacesQueryService _workplacesQueryService;

        public GetAllWorkplacesHandler(IWorkplacesQueryService workplacesQueryService)
        {
            _workplacesQueryService = workplacesQueryService;
        }

        public async Task<GetAllWorkplacesResponse> Handle(GetAllWorkplacesQuery request, CancellationToken cancellationToken)
        {
            var workplaces = await _workplacesQueryService.GetAllWorkplacesAsync(cancellationToken);

            return new GetAllWorkplacesResponse
            {
                AllWorkplacesList = workplaces
            };
        }
    }
}
