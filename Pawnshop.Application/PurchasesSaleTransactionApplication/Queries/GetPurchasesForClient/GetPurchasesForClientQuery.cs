using Pawnshop.Application.Common.Base;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient
{
    public sealed class GetPurchasesForClientQuery : BaseQuery<GetPurchasesForClientResponse>
    {
        public PaginationParameters PaginationParameters { get; set; }
    }
}
