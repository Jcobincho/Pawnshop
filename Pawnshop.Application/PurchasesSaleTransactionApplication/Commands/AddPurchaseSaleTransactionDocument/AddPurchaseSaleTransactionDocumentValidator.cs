using FluentValidation;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument
{
    public sealed class AddPurchaseSaleTransactionDocumentValidator : AbstractValidator<AddPurchaseSaleTransactionDocumentCommand>
    {
        public AddPurchaseSaleTransactionDocumentValidator()
        {
            RuleFor(x => x.TypeOfTransaction)
                .IsInEnum()
                .WithMessage("Invalid transaction type.");

            RuleFor(x => x.ClientId)
                .NotNull()
                .When(x => x.TypeOfTransaction == TypeOfTransactionEnum.Purchase)
                .WithMessage("Client ID is required for purchase transactions.");
        }
    }
}
