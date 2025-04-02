using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.EmployeesApplication.Interfaces
{
    public interface IEmployeesCommandService
    {
        Task<Guid> AddEmployeeAsync(AddEmployeeCommand command, CancellationToken cancellationToken);
        Task EditEmployeeAsync(EditEmployeeCommand command, CancellationToken cancellationToken);
        Task DeleteEmployeeAsync(DeleteEmployeeCommand command, CancellationToken cancellationToken);
    }
}
