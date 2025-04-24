using Pawnshop.Application.EmployeesApplication.Dto;
using Pawnshop.Domain.Entitie;

namespace Pawnshop.Application.EmployeesApplication.Interfaces
{
    public interface IEmployeesQueryService
    {
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId, CancellationToken cancellationToken);
        Task<List<EmployeeDto>> GetAllEmployeesAsDtoAsync(CancellationToken cancellationToken);
    }
}
