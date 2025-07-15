using Pawnshop.Application.Common.Base;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;
using Pawnshop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument
{
    public sealed class AddPurchaseSaleTransactionDocumentCommand : BaseCommand<AddPurchaseSaleTransactionDocumentResponse>
    {
        [Required(ErrorMessage = "Symbol is required.")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "Type of transaction is required.")]
        public TypeOfTransactionEnum TypeOfTransaction { get; set; }

        [Required(ErrorMessage = "Date of transaction is required.")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Workplace is required.")]
        public Guid WorkplaceId { get; set; }

        public Guid? ClientId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
