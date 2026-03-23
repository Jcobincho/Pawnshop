using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.ClientsApplication.Queries.GetAllClients;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Domain.Entities;

namespace Pawnshop.Application.ClientsApplication.Interfaces
{
    public interface IClientsQueryService
    {
        Task<Client> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken);
        Task<PagedResult<ClientDto>> GetAllClientsAsDtoAsync(GetAllClientsQuery query, CancellationToken cancellationToken);
        Task<bool> IsClientExistAsync(Guid clientId, CancellationToken cancellationToken);
    }
}
