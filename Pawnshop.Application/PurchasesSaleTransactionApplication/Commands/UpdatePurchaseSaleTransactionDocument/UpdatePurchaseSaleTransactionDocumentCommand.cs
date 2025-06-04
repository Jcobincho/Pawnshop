using Pawnshop.Application.Base;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;
using Pawnshop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument
{
    public sealed class UpdatePurchaseSaleTransactionDocumentCommand : BaseCommand<UpdatePurchaseSaleTransactionDocumentResponse>
    {
        [Required(ErrorMessage = "Transaction ID is required.")]
        public Guid PurchaseSaleTransactionDocumentId { get; set; }

        [Required(ErrorMessage = "Type of transaction is required.")]
        public TypeOfTransactionEnum TypeOfTransaction { get; set; }

        [Required(ErrorMessage = "Date of transaction is required.")]
        public DateTime TransactionDate { get; set; }
        public Guid? ClientId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
