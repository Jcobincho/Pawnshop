using Pawnshop.Domain.Entitie;

namespace Pawnshop.Application.EmployeesApplication.Dto.DtoExtension
{
    public static class EmployeeDtoExtension
    {
        public static EmployeeDto EmployeePraseToDto(this Employee employee)
        {

            return new EmployeeDto
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                SecondName = employee.SecondName,
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                CreatedAt = employee.CreatedAt,
                CreatedBy = employee.CreatedBy,
                EditedAt = employee.EditedAt,
                EditedBy = employee.EditedBy,
            };
        }
    }
}
