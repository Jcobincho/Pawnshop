using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Domain.Entities;

namespace Pawnshop.Application.ClientsApplication.Interfaces
{
    public interface IClientsQueryService
    {
        Task<Client> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);
        Task<List<ClientDto>> GetAllClientsAsDtoAsync(CancellationToken cancellationToken);
        Task<bool> IsClientExistAsync(Guid clientId, CancellationToken cancellationToken);
    }
}
