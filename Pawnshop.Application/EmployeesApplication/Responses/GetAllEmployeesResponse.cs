using Pawnshop.Application.EmployeesApplication.Dto;

namespace Pawnshop.Application.EmployeesApplication.Responses
{
    public sealed class GetAllEmployeesResponse
    {
        public List<EmployeeDto> AllEmployeesList { get; set; }
    }
}
