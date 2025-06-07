using Pawnshop.Application.Common.Base;

namespace Pawnshop.Application.EmployeesApplication.Dto
{
    public class EmployeeDto : BaseDto
    {
        public Guid EmployeeId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.Today;
    }
}
