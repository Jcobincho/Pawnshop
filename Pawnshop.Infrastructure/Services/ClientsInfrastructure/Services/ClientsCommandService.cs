using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Commands.AddClient;
using Pawnshop.Application.ClientsApplication.Commands.DeleteClient;
using Pawnshop.Application.ClientsApplication.Commands.UpdateClient;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ClientsInfrastructure.Services
{
    internal sealed class ClientsCommandService : IClientsCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IClientsQueryService _clientsQueryService;

        public ClientsCommandService(DbContext dbContext, IClientsQueryService clientsQueryService)
        {
            _dbContext = dbContext;
            _clientsQueryService = clientsQueryService;
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
            var client = await _clientsQueryService.GetClientByIdAsync(command.ClientId, cancellationToken);

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
            var client = await _clientsQueryService.GetClientByIdAsync(command.ClientId, cancellationToken);

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
