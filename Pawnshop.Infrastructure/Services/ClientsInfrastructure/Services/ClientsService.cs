using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Commands.AddClient;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
    }
}
