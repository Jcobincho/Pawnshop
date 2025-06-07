using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient;
using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces
{
    public interface IPurchasesSaleTransactionQueryService
    {
        Task<PurchaseSaleTransaction> GetPurchaseSaleTransactionByIdAsync(Guid purchaseSateTransactionId, CancellationToken cancellationToken);
        Task<PagedResult<SalesTransactionDto>> GetEverySalesTransactionsPagedAsDtoAsync(GetEverySalesTransactionQuery query,CancellationToken cancellationToken);
        Task<PagedResult<PurchasesTransactionDto>> GetPurchasesForClientPagedAsDtoAsync(GetPurchasesForClientQuery query, CancellationToken cancellationToken);
    }
}
