using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Producers;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GenerateAgreement
{
    public sealed class GenerateAgreementHandler : IRequestHandler<GenerateAgreementQuery, GenerateAgreementResponse>
    {
        private readonly IPurchaseSaleTransactionEventPublisher _purchaseSaleTransactionEventPublisher;

        public GenerateAgreementHandler(IPurchaseSaleTransactionEventPublisher purchaseSaleTransactionEventPublisher)
        {
            _purchaseSaleTransactionEventPublisher = purchaseSaleTransactionEventPublisher;
        }

        public async Task<GenerateAgreementResponse> Handle(GenerateAgreementQuery request, CancellationToken cancellationToken)
        {
            var purchaseSaleTransactionEvent = new GenerateAgreementEvent(request.PurchasesSaleTransactionId, request.UserIdFromClaims);

            await _purchaseSaleTransactionEventPublisher.GenerateAgreementPublishAsync(purchaseSaleTransactionEvent, cancellationToken);

            return new GenerateAgreementResponse();
        }
    }
}
