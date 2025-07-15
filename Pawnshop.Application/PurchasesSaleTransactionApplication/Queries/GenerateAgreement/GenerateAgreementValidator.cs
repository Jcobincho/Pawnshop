using FluentValidation;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GenerateAgreement
{
    public sealed class GenerateAgreementValidator : AbstractValidator<GenerateAgreementQuery>
    {
        public GenerateAgreementValidator()
        {
            RuleFor(x => x.PurchasesSaleTransactionId)
                .NotEmpty()
                .WithMessage("Purchase sale transaction ID is required.");
        }
    }
}
