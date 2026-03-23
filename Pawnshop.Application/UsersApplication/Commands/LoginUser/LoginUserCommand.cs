using Pawnshop.Application.Common.Base;
using Pawnshop.Domain.AuthTokens;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.UsersApplication.Commands.LoginUser
{
    public class LoginUserCommand : BaseCommand<JsonWebToken>
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
