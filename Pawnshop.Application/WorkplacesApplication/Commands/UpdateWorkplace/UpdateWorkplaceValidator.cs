using FluentValidation;

namespace Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace
{
    public sealed class UpdateWorkplaceValidator : AbstractValidator<UpdateWorkplaceCommand>
    {
        public UpdateWorkplaceValidator()
        {
            RuleFor(x => x.ZipCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Incorrect zip code format for Poland");
        }
    }
}
