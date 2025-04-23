using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.DeleteUser
{
    public sealed class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.UserId).NotNull()
                                  .NotEmpty()
                                  .WithMessage("User id is required.");
        }
    }
}
