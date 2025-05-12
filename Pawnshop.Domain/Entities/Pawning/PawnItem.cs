using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Pawning
{
    public class PawnItem : BaseEntity
    {
        public Guid PawnAgreementId { get; set; }
        public PawnAgreement PawnAgreement { get; set; }

        public Guid ItemDetailId { get; set; }
        public ItemDetail ItemDetail { get; set; }

        public float ItemPrice { get; set; }
        public PawnedItemStatus Status { get; set; }
    }
}
