using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Domain.Entities;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services
{
    internal sealed class EmployeesService : IEmployeesCommandService, IEmployeesQueryService
    {
        private readonly DbContext _dbContext;

        public EmployeesService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddEmployeeAsync(AddEmployeeCommand command, CancellationToken cancellationToken)
        {
            var newEmployee = new Employees()
            {
                Name = command.Name,
                SecondName = command.SecondName,
                Surname = command.Surname,
                BirthDate = command.BirthDate
            };

            await _dbContext.Employee.AddAsync(newEmployee, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newEmployee.Id;
        }

        public async Task EditEmployeeAsync(EditEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await GetEmployeesByIdAsync(command.EmployeeId, cancellationToken);

            employee.Name = command.Name;
            employee.SecondName = command.SecondName;
            employee.Surname = command.Surname;
            employee.BirthDate = command.BirthDate;

            _dbContext.Employee.Update(employee);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteEmployeeAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await GetEmployeesByIdAsync(command.EmployeeId, cancellationToken);

            _dbContext.Employee.Remove(employee);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Employees> GetEmployeesByIdAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employee.FindAsync(employeeId, cancellationToken);

            if(employee == null)
            {
                throw new NotFoundException("Empolyee doesn't exist.");
            }

            return employee;
        }
    }
}
