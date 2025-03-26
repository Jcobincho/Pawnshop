﻿using FluentValidation;

namespace Pawnshop.Application.UsersApplication.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.RepeatedPassword)
                .Equal(x => x.Password)
                .WithMessage("Password not equal.");
        }
    }
}
