using FluentValidation;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument
{
    public sealed class UpdatePurchaseSaleTransactionDocumentValidator : AbstractValidator<UpdatePurchaseSaleTransactionDocumentCommand>
    {
        public UpdatePurchaseSaleTransactionDocumentValidator()
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
