using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ClientsApplication.Commands.AddClient
{
    public sealed class AddClientValidator : AbstractValidator<AddClientCommand>
    {
        public AddClientValidator()
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

            RuleFor(x => x.BirthDate)
                .Must(BeAtLeast18)
                .WithMessage("Client must be at least 18 years old.");
        }

        private bool BeAtLeast18(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;
            return age >= 18;
        }
    }
}
