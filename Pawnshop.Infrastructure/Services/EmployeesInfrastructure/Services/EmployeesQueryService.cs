using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.EmployeesApplication.Dto;
using Pawnshop.Application.EmployeesApplication.Dto.DtoExtension;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Domain.Entitie;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services
{
    internal sealed class EmployeesQueryService : IEmployeesQueryService
    {
        private readonly DbContext _dbContext;

        public EmployeesQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException("Empolyee doesn't exist.");
            }

            return employee;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsDtoAsync(CancellationToken cancellationToken)
        {
            var employees = await _dbContext.Employees.Select(x => x.EmployeePraseToDto()).ToListAsync(cancellationToken);

            return employees;
        }
    }
}
