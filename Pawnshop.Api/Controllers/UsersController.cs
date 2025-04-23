using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.DeleteUser;
using Pawnshop.Application.UsersApplication.Commands.EditUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Commands.RefreshToken;
using Pawnshop.Application.UsersApplication.Queries.GetAllUsers;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : BaseController<CreateUserCommand,
                                                  UpdateUserCommand,
                                                  DeleteUserCommand,
                                                  GetAllUsersQuery>
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command, CancellationToken cancellation)
        {
            var response = await Sender.Send(command, cancellation);

            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = response.RefreshToken.Expires
            };

            Response.Cookies.Append("refresh-token", response.RefreshToken.Token, cookie);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command,CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken)
        {
            var resposne = await Sender.Send(command, cancellationToken);

            return Ok(resposne);
        }
    }
}
