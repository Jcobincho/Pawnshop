using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction
{
    public sealed class GetEverySalesTransactionHandler : IRequestHandler<GetEverySalesTransactionQuery, GetEverySalesTransactionResponse>
    {
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;

        public GetEverySalesTransactionHandler(IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService)
        {
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
        }

        public async Task<GetEverySalesTransactionResponse> Handle(GetEverySalesTransactionQuery request, CancellationToken cancellationToken)
        {
            var pagedSalesTransactions = await _purchasesSaleTransactionQueryService.GetEverySalesTransactionsPagedAsDtoAsync(request, cancellationToken);

            return new GetEverySalesTransactionResponse()
            {
                SalesTransactionPagedList = pagedSalesTransactions,
            };
        }
    }
}
