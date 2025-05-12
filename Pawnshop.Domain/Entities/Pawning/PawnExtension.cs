namespace Pawnshop.Domain.Entities.Pawning
{
    public class PawnExtension : BaseEntity
    {
        public Guid PawnAgreementId { get; set; }
        public PawnAgreement PawnAgreement { get; set; }

        public DateTime ExtensionDate { get; set; }
        public DateTime ExtensionDateTo { get; set; }
        public float PaidPrice { get; set; }
        public string Comment { get; set; }
    }
}
