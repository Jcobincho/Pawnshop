using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Responses
{
    public sealed class GetPurchasesForClientResponse
    {
        public PagedResult<PurchasesTransactionDto> PurchasesPagedList { get; set; }
    }
}
