using Pawnshop.Application.Base;
using Pawnshop.Application.UsersApplication.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.EditUser
{
    public sealed class EditUserCommand : BaseCommand<EditUserResponse>
    {
        [Required(ErrorMessage = "User identifier is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName in required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string RepeatedPassword { get; set; }

        public Guid EmployeeId { get; set; } = new Guid();

        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
