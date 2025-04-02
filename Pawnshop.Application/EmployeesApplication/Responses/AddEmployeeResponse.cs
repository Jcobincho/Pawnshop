using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.EmployeesApplication.Responses
{
    public sealed class AddEmployeeResponse
    {
        public Guid EmpoyeeId { get; set; }
        public string Message { get; set; } = "Success.";
    }
}
