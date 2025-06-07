using Pawnshop.Application.Common.Base;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient
{
    public sealed class GetPurchasesForClientQuery : BaseQuery<GetPurchasesForClientResponse>
    {
        [Required(ErrorMessage = "Client ID is required.")]
        public Guid ClientId { get; set; }
        public PaginationParameters PaginationParameters { get; set; }
    }
}
