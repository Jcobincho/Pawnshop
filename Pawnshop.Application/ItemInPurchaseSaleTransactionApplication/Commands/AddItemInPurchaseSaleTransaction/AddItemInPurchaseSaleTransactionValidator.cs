using FluentValidation;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction
{
    public sealed class AddItemInPurchaseSaleTransactionValidator : AbstractValidator<AddItemInPurchaseSaleTransactionCommand>
    {
        public AddItemInPurchaseSaleTransactionValidator()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .When(x => x.AddItemHistory)
                .WithMessage("Description is required when adding item history.");

            RuleFor(x => x.AddItemHistory)
                .Equal(true)
                .When(x => x.AddItemValuation)
                .WithMessage("Item history must be added when adding valuation.");

            RuleFor(x => x.ItemValuationPrice)
                .GreaterThan(0)
                .When(x => x.AddItemValuation)
                .WithMessage("Valuation price must be greater than 0.");

            RuleFor(x => x.Justification)
                .NotNull()
                .When(x => x.AddItemValuation)
                .WithMessage("Justification is required for valuation.");
        }
    }
}
