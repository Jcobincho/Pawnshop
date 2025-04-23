using Pawnshop.Application.EmployeesApplication.Dto;
using Pawnshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.EmployeesApplication.Interfaces
{
    public interface IEmployeesQueryService
    {
        Task<Employee> GetEmployeesByIdAsync(Guid employeeId, CancellationToken cancellationToken);
        Task<List<EmployeeDto>> GetAllEmployeesAsDtoAsync(CancellationToken cancellationToken);
    }
}
