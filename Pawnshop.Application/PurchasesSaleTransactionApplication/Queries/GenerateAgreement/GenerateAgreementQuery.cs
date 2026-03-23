using Pawnshop.Application.Common.Base;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GenerateAgreement
{
    public sealed class GenerateAgreementQuery : BaseQuery<GenerateAgreementResponse>
    {
        public Guid PurchasesSaleTransactionId { get; set; }
    }
}
