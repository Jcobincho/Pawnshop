using Pawnshop.Application.ItemDetailsApplication.Dto;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Responses
{
    public sealed class GetItemsForPurchaseSaleTransactionResponse
    {
        public List<ItemInPurchaseSaleTransactionDto> ItemsForPurchaseSaleTransactionList { get; set; }
    }
}
