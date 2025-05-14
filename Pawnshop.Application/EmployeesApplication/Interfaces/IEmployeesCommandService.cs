using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;

namespace Pawnshop.Application.EmployeesApplication.Interfaces
{
    public interface IEmployeesCommandService
    {
        Task<Guid> AddEmployeeAsync(AddEmployeeCommand command, CancellationToken cancellationToken);
        Task EditEmployeeAsync(UpdateEmployeeCommand command, CancellationToken cancellationToken);
        Task DeleteEmployeeAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken);
    }
}
