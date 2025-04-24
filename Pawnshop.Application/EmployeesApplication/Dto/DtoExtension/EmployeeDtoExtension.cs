using Pawnshop.Domain.Entitie;

namespace Pawnshop.Application.EmployeesApplication.Dto.DtoExtension
{
    public static class EmployeeDtoExtension
    {
        public static EmployeeDto EmployeePraseToDto(this Employee employees)
        {

            return new EmployeeDto
            {
                EmployeeId = employees.Id,
                Name = employees.Name,
                SecondName = employees.SecondName,
                Surname = employees.Surname,
                BirthDate = employees.BirthDate,
                CreatedAt = employees.CreatedAt,
                CreatedBy = employees.CreatedBy,
                EditedAt = employees.EditedAt,
                EditedBy = employees.EditedBy,
            };
        }
    }
}
