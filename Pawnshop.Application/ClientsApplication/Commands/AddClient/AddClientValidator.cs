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

            RuleFor(x => x.Pesel)
                .Length(9).WithMessage("Telephone number must contain exactly 11 characters.")
                .Matches(@"^\d+$").WithMessage("Telephone number must contain only numbers.");
        }
    }
}
