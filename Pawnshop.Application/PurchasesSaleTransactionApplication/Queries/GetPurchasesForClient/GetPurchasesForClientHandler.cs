using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient
{
    public sealed class GetPurchasesForClientHandler : IRequestHandler<GetPurchasesForClientQuery, GetPurchasesForClientResponse>
    {
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;

        public GetPurchasesForClientHandler(IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService)
        {
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
        }

        public async Task<GetPurchasesForClientResponse> Handle(GetPurchasesForClientQuery request, CancellationToken cancellationToken)
        {
            var pagedPurchases = await _purchasesSaleTransactionQueryService.GetPurchasesForClientPagedAsDtoAsync(request, cancellationToken);

            return new GetPurchasesForClientResponse
            {
                PurchasesPagedList = pagedPurchases
            };
        }
    }
}
