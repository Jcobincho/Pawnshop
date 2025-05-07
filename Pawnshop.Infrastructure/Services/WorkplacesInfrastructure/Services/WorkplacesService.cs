using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace;
using Pawnshop.Application.WorkplacesApplication.Dto;
using Pawnshop.Application.WorkplacesApplication.Dto.DtoExtension;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services
{
    internal sealed class WorkplacesService : IWorkplacesCommandService, IWorkplacesQueryService
    {
        private readonly DbContext _dbContext;

        public WorkplacesService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddWorkplaceAsync(AddWorkplaceCommand command, CancellationToken cancellationToken)
        {
            Workplace newWorkplace = new Workplace()
            {
                Country = command.Country,
                Region = command.Region,
                StreetAndBuildingNumber = command.StreetAndBuildingNumber,
                ZipCode = command.ZipCode,
                City = command.City,
            };

            await _dbContext.Workplaces.AddAsync(newWorkplace, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newWorkplace.Id;
        }

        public async Task UpdateWorkplaceAsync(UpdateWorkplaceCommand command, CancellationToken cancellationToken)
        {
            var workplace = await GetWorkplaceByIdAsync(command.WorkplaceId, cancellationToken);

            workplace.Country = command.Country;
            workplace.Region = command.Region;
            workplace.StreetAndBuildingNumber = command.StreetAndBuildingNumber;
            workplace.ZipCode = command.ZipCode;
            workplace.City = command.City;

            _dbContext.Workplaces.Update(workplace);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteWorkplaceAsync(DeleteWorkplaceCommand command, CancellationToken cancellationToken)
        {
            var workplace = await GetWorkplaceByIdAsync(command.WorkplaceId, cancellationToken);

            _dbContext.Workplaces.Remove(workplace);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Workplace> GetWorkplaceByIdAsync(Guid workplaceId, CancellationToken cancellationToken)
        {
            var workplace = await _dbContext.Workplaces.FindAsync(workplaceId, cancellationToken);

            if (workplace == null)
                throw new NotFoundException("Workplace doesn't exist.");

            return workplace;
        }

        public async Task<List<WorkplaceDto>> GetAllWorkplacesAsync(CancellationToken cancellationToken)
        {
            var workplaces = await _dbContext.Workplaces.Select(x => x.WorkplaceParseToDto())
                                                        .ToListAsync(cancellationToken);

            return workplaces;
        }
    }
}
