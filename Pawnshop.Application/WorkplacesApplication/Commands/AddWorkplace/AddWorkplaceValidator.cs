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

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90d, 90d)
                .When(x => x.Latitude.HasValue);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180d, 180d)
                .When(x => x.Longitude.HasValue);
        }
    }
}
