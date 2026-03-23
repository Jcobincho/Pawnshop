using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Domain.Entitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.EmployeesInfrastructure.Services
{
    internal sealed class EmployeesCommandService : IEmployeesCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IUsersCommandService _usersCommandService;
        private readonly IEmployeesQueryService _employeesQueryService;

        public EmployeesCommandService(DbContext dbContext, IUsersCommandService usersCommandService, IEmployeesQueryService employeesQueryService)
        {
            _dbContext = dbContext;
            _usersCommandService = usersCommandService;
            _employeesQueryService = employeesQueryService;
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
            var employee = await _employeesQueryService.GetEmployeeByIdAsync(command.EmployeeId, cancellationToken);

            employee.Name = command.Name;
            employee.SecondName = command.SecondName;
            employee.Surname = command.Surname;
            employee.BirthDate = command.BirthDate;

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteEmployeeAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _employeesQueryService.GetEmployeeByIdAsync(command.EmployeeId, cancellationToken);

            await _usersCommandService.UpdateEmployeeIdentifierAsync(command.EmployeeId, cancellationToken);

            _dbContext.Employees.Remove(employee);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
