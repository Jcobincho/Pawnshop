using Pawnshop.Domain.Entitie;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Transactions
{
    public class PurchaseSaleTransaction : BaseEntity
    {
        public TypeOfTransactionEnum TypeOfTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public float TotalPrice { get; set; }
        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
        public string Description { get; set; }
    }
}
