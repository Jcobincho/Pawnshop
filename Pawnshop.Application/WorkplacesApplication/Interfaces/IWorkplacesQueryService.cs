using Pawnshop.Application.WorkplacesApplication.Dto;
using Pawnshop.Domain.Entities;

namespace Pawnshop.Application.WorkplacesApplication.Interfaces
{
    public interface IWorkplacesQueryService
    {
        Task<Workplace> GetWorkplaceByIdAsync(Guid workplaceId, CancellationToken cancellationToken);
        Task<List<WorkplaceDto>> GetAllWorkplacesAsync(CancellationToken cancellationToken);
        Task<bool> WorkplaceExistsAsync(Guid workplaceId, CancellationToken cancellationToken);
    }
}
