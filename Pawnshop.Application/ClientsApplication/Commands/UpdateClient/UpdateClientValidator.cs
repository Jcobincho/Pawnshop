using FluentValidation;

namespace Pawnshop.Application.ClientsApplication.Commands.UpdateClient
{
    public sealed class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientValidator()
        {
            RuleFor(x => x.Pesel)
                .NotEmpty().WithMessage("PESEL is required.")
                .Length(11).WithMessage("PESEL must contain exactly 11 characters.")
                .Matches(@"^\d+$").WithMessage("PESEL must contain only numbers.");

            RuleFor(x => x.TelephoneNumber)
            .Length(9).WithMessage("Telephone number must contain exactly 9 characters.")
                .When(x => !string.IsNullOrEmpty(x.TelephoneNumber))
            .Matches(@"^\d+$").WithMessage("Telephone number must contain only numbers.")
                .When(x => !string.IsNullOrEmpty(x.TelephoneNumber));
        }
    }
}
