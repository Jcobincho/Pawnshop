using Pawnshop.Application.WorkplacesApplication.Dto;

namespace Pawnshop.Application.WorkplacesApplication.Responses
{
    public sealed class GetAllWorkplacesResponse
    {
        public List<WorkplaceDto> AllWorkplacesList { get; set; }
    }
}
