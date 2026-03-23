using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Queries.GetItemsForPurchaseSaleTransaction
{
    public sealed class GetItemsForPurchaseSaleTransactionQuery : BaseQuery<GetItemsForPurchaseSaleTransactionResponse>
    {
        [Required(ErrorMessage = "Document ID is required.")]
        public Guid PurchaseSaleTransactionId { get; set; }
    }
}
