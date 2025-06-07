using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Responses
{
    public sealed class GetEverySalesTransactionResponse
    {
        public PagedResult<SalesTransactionDto> SalesTransactionPagedList { get; set; }
    }
}
