using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Pawning
{
    public class PawnDebtRepayment : BaseEntity
    {
        public Guid PawnAgreementId { get; set; }
        public PawnAgreement PawnAgreement { get; set; }

        public DateTime DebtRepaymentDate { get; set; }
        public float DebtRepaymentPrice { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public bool FullRepayment { get; set; }
    }
}
