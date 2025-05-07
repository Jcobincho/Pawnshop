using FluentValidation;

namespace Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace
{
    public sealed class AddWorkplaceValidator : AbstractValidator<AddWorkplaceCommand>
    {
        public AddWorkplaceValidator()
        {
            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Incorrect zip code format for Poland");
        }
    }
}
