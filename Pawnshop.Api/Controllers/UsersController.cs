﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pawnshop.Application.Base;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Commands.RefreshToken;

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
            //var refreshToken = Request.Cookies["refresh-token"];

            //var command = new RefreshTokenCommand()
            //{
            //    RefreshToken = refreshToken
            //};

            var response = await Sender.Send(command, cancellationToken);

            //var cookie = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Expires = response.RefreshToken.Expires
            //};

            //Response.Cookies.Append("refresh-token", response.RefreshToken.Token, cookie);

            return Ok(response);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken)
        {
            //var refreshToken = Request.Cookies["refresh-token"];

            //var command = new LogoutCommand()
            //{
            //    RefreshToken = refreshToken
            //};

            var resposne = await Sender.Send(command, cancellationToken);

            //Response.Cookies.Delete("refresh-token");

            return Ok(resposne);
        }

        [HttpPost("test")]
        [Authorize]
        public async Task<IActionResult> Test([FromBody] TestClassPublic test)
        { 
            return Ok(new TestClassRTesponse() { REsponseTEst = "OK"});
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
