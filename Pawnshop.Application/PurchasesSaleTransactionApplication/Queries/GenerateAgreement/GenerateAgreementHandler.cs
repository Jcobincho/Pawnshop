using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GenerateAgreement
{
    public sealed class GenerateAgreementHandler : IRequestHandler<GenerateAgreementQuery, GenerateAgreementResponse>
    {
        public async Task<GenerateAgreementResponse> Handle(GenerateAgreementQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
