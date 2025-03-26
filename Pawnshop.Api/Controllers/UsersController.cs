﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : BaseController<CreateUserCommand,
                                                  BaseCommand,
                                                  BaseCommand,
                                                  BaseQuery>
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command, CancellationToken cancellation)
        {
            var response = await Sender.Send(command, cancellation);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["refresh-token"];
            var response = await Sender.Send(new RefreshTokenCommand(refreshToken), cancellationToken);

            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = response.RefreshToken.Expires
            };

            Response.Cookies.Append("refresh-token", response.RefreshToken.Token, cookie);

            return Ok(response);
        }


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
