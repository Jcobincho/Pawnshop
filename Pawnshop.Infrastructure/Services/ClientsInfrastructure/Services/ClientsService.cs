using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Commands.AddClient;
using Pawnshop.Application.ClientsApplication.Commands.DeleteClient;
using Pawnshop.Application.ClientsApplication.Commands.UpdateClient;
using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.ClientsApplication.Dto.DtoExtension;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ClientsInfrastructure.Services
{
    internal sealed class ClientsService : IClientsCommandService, IClientsQueryService
    {
        private readonly DbContext _dbContext;

        public ClientsService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddClientAsync(AddClientCommand command, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.AnyAsync(x => x.Pesel == command.Pesel, cancellationToken);

            if (client)
                throw new BadRequestException("Client already exists.");

            var newClinet = new Client()
            {
                Name = command.Name,
                SecondName = command.SecondName,
                Surname = command.Surname,
                BirthDate = command.BirthDate,
                Pesel = command.Pesel,
                IdCardNumber = command.IdCardNumber,
                TelephoneNumber = command.TelephoneNumber,
                Email = command.Email,
                Description = command.Description
            };

            await _dbContext.Clients.AddAsync(newClinet, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newClinet.Id;
        }

        public async Task UpdateClientAsync(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            var client = await GetClientByIdAsync(command.ClientId, cancellationToken);

            client.Name = command.Name;
            client.SecondName = command.SecondName;
            client.Surname = command.Surname;
            client.BirthDate = command.BirthDate;
            client.Pesel = command.Pesel;
            client.IdCardNumber = command.IdCardNumber;
            client.TelephoneNumber = command.TelephoneNumber;
            client.Email = command.Email;
            client.Description = command.Description;

            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteClientAsync(DeleteClientCommand command, CancellationToken cancellationToken)
        {
            var client = await GetClientByIdAsync(command.ClientId, cancellationToken);

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
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
