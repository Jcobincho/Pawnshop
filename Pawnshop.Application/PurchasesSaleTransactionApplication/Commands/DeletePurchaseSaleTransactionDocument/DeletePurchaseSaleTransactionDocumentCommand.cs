using Pawnshop.Application.Base;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument
{
    public sealed class DeletePurchaseSaleTransactionDocumentCommand : BaseCommand<DeletePurchaseSaleTransactionDocumentResponse>
    {
        [Required(ErrorMessage = "Document ID is required.")]
        public Guid PurchaseSaleTransactionDocumentId { get; set; }
    }
}
