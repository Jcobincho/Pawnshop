using FluentValidation;

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
