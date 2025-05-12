using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Pawning
{
    public class PawnAgreement : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public DateTime AgreementDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public float LoanAmount { get; set; }
        public float Interest { get; set; }
        public float StorageFee { get; set; }
        public PawningStatusEnum Status { get; set; }
        public string ContractTerms { get; set; }
    }
}
