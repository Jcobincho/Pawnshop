using Pawnshop.Application.Common.Base;
using Pawnshop.Application.EmployeesApplication.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee
{
    public sealed class DeleteEmployeeCommand : BaseCommand<DeleteEmployeeResponse>
    {
        [Required(ErrorMessage = "Employee identifier is required.")]
        public Guid EmployeeId { get; set; }
    }
}
