using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Commands.EditUser
{
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.RepeatedPassword)
                .Equal(x => x.Password)
                .WithMessage("Password not equal.");
        }
    }
}
