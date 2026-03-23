using Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services
{
    internal sealed class WorkplacesCommandService : IWorkplacesCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IWorkplacesQueryService _workplacesQueryService;

        public WorkplacesCommandService(DbContext dbContext, IWorkplacesQueryService workplacesQueryService)
        {
            _dbContext = dbContext;
            _workplacesQueryService = workplacesQueryService;
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
            var workplace = await _workplacesQueryService.GetWorkplaceByIdAsync(command.WorkplaceId, cancellationToken);

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
            var workplace = await _workplacesQueryService.GetWorkplaceByIdAsync(command.WorkplaceId, cancellationToken);

            _dbContext.Workplaces.Remove(workplace);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
