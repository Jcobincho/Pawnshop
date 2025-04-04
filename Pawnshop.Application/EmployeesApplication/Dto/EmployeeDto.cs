using Pawnshop.Application.Base;

namespace Pawnshop.Application.EmployeesApplication.Dto
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
