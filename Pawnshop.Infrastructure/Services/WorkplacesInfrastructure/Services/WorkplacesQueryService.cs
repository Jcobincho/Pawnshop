using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.Common.Mapper;
using Pawnshop.Application.WorkplacesApplication.Dto;
using Pawnshop.Application.WorkplacesApplication.Dto.DtoExtension;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.WorkplacesInfrastructure.Services
{
    internal sealed class WorkplacesQueryService : IWorkplacesQueryService
    {
        private readonly DbContext _dbContext;

        public WorkplacesQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
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
            var baseQuery = _dbContext.Workplaces.AsNoTracking();

            var workplaces = await baseQuery.MapTo<WorkplaceDto>().ToListAsync(cancellationToken);

            return workplaces;
        }

        public async Task<bool> WorkplaceExistsAsync(Guid workplaceId, CancellationToken cancellationToken)
        {
            return await _dbContext.Workplaces.AnyAsync(x => x.Id == workplaceId, cancellationToken);
        }
    }
}
