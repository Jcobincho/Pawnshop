
using Pawnshop.Application.Base;
using Pawnshop.Application.Users.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.UsersApplication.Commands.CreateUser
{
    public sealed class CreateUserCommand : BaseCommand<CreateUserResponse>
    {
        [Required(ErrorMessage = "UserName in required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeated password is required.")]
        public string RepeatedPassword { get; set; }

        public Guid EmployeeId { get; set; } = new Guid();

        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
