using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    public class EmployeesController : BaseController<AddEmployeeCommand, EditEmployeeCommand, DeleteEmployeeCommand, BaseQuery>
    {
        [NonAction]
        public override Task<IActionResult> GetAsync([FromQuery] BaseQuery data, CancellationToken cancellation)
        {
            return base.GetAsync(data, cancellation);
        }
    }
}
