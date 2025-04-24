using Pawnshop.Application.EmployeesApplication.Dto;
using Pawnshop.Domain.Entitie;

namespace Pawnshop.Application.EmployeesApplication.Interfaces
{
    public interface IEmployeesQueryService
    {
        Task<Employee> GetEmployeesByIdAsync(Guid employeeId, CancellationToken cancellationToken);
        Task<List<EmployeeDto>> GetAllEmployeesAsDtoAsync(CancellationToken cancellationToken);
    }
}
