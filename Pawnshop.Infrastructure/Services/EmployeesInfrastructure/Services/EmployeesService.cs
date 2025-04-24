using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;
using Pawnshop.Application.EmployeesApplication.Dto;
using Pawnshop.Application.EmployeesApplication.Dto.DtoExtension;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Domain.Entitie;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services
{
    internal sealed class EmployeesService : IEmployeesCommandService, IEmployeesQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IUsersCommandService _usersCommandService;

        public EmployeesService(DbContext dbContext, IUsersCommandService usersCommandService)
        {
            _dbContext = dbContext;
            _usersCommandService = usersCommandService;
        }

        public async Task<Guid> AddEmployeeAsync(AddEmployeeCommand command, CancellationToken cancellationToken)
        {
            var newEmployee = new Employee()
            {
                Name = command.Name,
                SecondName = command.SecondName,
                Surname = command.Surname,
                BirthDate = command.BirthDate
            };

            await _dbContext.Employees.AddAsync(newEmployee, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newEmployee.Id;
        }

        public async Task EditEmployeeAsync(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await GetEmployeesByIdAsync(command.EmployeeId, cancellationToken);

            employee.Name = command.Name;
            employee.SecondName = command.SecondName;
            employee.Surname = command.Surname;
            employee.BirthDate = command.BirthDate;

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteEmployeeAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await GetEmployeesByIdAsync(command.EmployeeId, cancellationToken);

            await _usersCommandService.UpdateEmployeeIdentifierAsync(command.EmployeeId, cancellationToken);

            _dbContext.Employees.Remove(employee);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Employee> GetEmployeesByIdAsync(Guid employeeId, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId, cancellationToken);

            if(employee == null)
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
