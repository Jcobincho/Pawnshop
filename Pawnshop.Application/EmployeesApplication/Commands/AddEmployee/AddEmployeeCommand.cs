using Pawnshop.Application.Base;
using Pawnshop.Application.EmployeesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.EmployeesApplication.Commands.AddEmployee
{
    public sealed class AddEmployeeCommand : BaseCommand<AddEmployeeResponse>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Birthday date is required.")]
        public DateTime BirthDate { get; set; }
    }
}
