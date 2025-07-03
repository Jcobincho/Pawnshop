using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.ClientsApplication.Dto.DtoExtension;
using Pawnshop.Application.ClientsApplication.Interfaces;
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

        public async Task<List<ClientDto>> GetAllClientsAsDtoAsync(CancellationToken cancellationToken)
        {
            var clients = await _dbContext.Clients.Select(x => x.ClientParseToDto()).ToListAsync(cancellationToken);

            return clients;
        }

        public async Task<bool> IsClientExistAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.AnyAsync(x => x.Id == clientId, cancellationToken);
        }
    }
}
