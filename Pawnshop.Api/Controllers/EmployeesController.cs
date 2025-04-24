using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.EmployeesApplication.Commands.AddEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee;
using Pawnshop.Application.EmployeesApplication.Commands.EditEmployee;
using Pawnshop.Application.EmployeesApplication.Queries.GetAllEmployees;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class EmployeesController : BaseController<AddEmployeeCommand, UpdateEmployeeCommand, DeleteEmployeeCommand, GetAllEmployeesQuery>
    {

    }
}
