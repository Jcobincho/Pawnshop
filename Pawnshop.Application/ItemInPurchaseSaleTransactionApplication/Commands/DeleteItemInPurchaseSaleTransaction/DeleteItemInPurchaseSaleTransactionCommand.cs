using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.DeleteItemInPurchaseSaleTransaction
{
    public sealed class DeleteItemInPurchaseSaleTransactionCommand : BaseCommand<DeleteItemInPurchaseSaleTransactionResponse>
    {
        [Required(ErrorMessage = "Item transaction ID is required.")]
        public Guid ItemInPurchaseSaleTransactionId { get; set; }
    }
}
