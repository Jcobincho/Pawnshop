using Pawnshop.Application.Common.Base;
using Pawnshop.Application.EmployeesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee
{
    public sealed class DeleteEmployeeCommand : BaseCommand<DeleteEmployeeResponse>
    {
        [Required(ErrorMessage = "Employee identifier is required.")]
        public Guid EmployeeId { get; set; }
    }
}
