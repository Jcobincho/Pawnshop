using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction
{
    public sealed class AddItemInPurchaseSaleTransactionCommand : BaseCommand<AddItemInPurchaseSaleTransactionResponse>
    {
        [Required(ErrorMessage = "Document transaction is required.")]
        public Guid PurchaseSaleTransactionId { get; set; }

        [Required(ErrorMessage = "Item is required.")]
        public Guid ItemDetailId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public float ItemPrice { get; set; }

        // DATA TO ITEM HISTORY
        public bool AddItemHistory { get; set; } = false;
        public string Description { get; set; } = string.Empty;

        // DATA TO ITEM VALUATION
        public bool AddItemValuation { get; set; } = false;
        public float ItemValuationPrice { get; set; } = 0;
        public string Justification { get; set; } = string.Empty;
    }
}
