using Pawnshop.Application.Common.Base;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction
{
    public sealed class GetEverySalesTransactionQuery : BaseQuery<GetEverySalesTransactionResponse>
    {
        public PaginationParameters PaginationParameters { get; set; }
    }
}
