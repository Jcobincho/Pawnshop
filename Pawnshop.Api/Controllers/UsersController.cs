using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : BaseController<CreateUserCommand,
                                                  BaseCommand,
                                                  BaseCommand,
                                                  BaseQuery>
    {
        [NonAction]
        public override Task<IActionResult> GetAsync([FromQuery] BaseQuery data, CancellationToken cancellation)
        {
            return base.GetAsync(data, cancellation);
        }

        [NonAction]
        public override Task<IActionResult> DeteleAsync([FromBody] BaseCommand data, CancellationToken cancellation)
        {
            return base.DeteleAsync(data, cancellation);
        }

        [NonAction]
        public override Task<IActionResult> PutAsync([FromBody] BaseCommand data, CancellationToken cancellation)
        {
            return base.PutAsync(data, cancellation);
        }
    }
}
