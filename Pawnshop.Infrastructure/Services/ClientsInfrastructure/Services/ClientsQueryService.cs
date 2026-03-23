using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.ClientsApplication.Queries.GetAllClients;
using Pawnshop.Application.Common.Mapper;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ClientsInfrastructure.Services
{
    internal sealed class ClientsQueryService : IClientsQueryService
    {
        private readonly DbContext _dbContext;

        public ClientsQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetClientByIdAsync(Guid clientId, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FindAsync(clientId, cancellationToken);

            if (client == null)
                throw new NotFoundException("Client doesn't exist.");

            return client;
        }

        public async Task<PagedResult<ClientDto>> GetAllClientsAsDtoAsync(GetAllClientsQuery query, CancellationToken cancellationToken)
        {
            var baseQuery = _dbContext.Clients.AsNoTracking();

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            var clients = await baseQuery.OrderBy(c => c.Surname)
                                         .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                                         .Take(query.PaginationParameters.PageSize)
                                         .MapTo<ClientDto>()
                                         .ToListAsync(cancellationToken);

            return new PagedResult<ClientDto>
            (
                clients,
                totalCount,
                query.PaginationParameters.PageNumber,
                query.PaginationParameters.PageSize
            );
        }

        public async Task<bool> IsClientExistAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.AnyAsync(x => x.Id == clientId, cancellationToken);
        }
    }
}
