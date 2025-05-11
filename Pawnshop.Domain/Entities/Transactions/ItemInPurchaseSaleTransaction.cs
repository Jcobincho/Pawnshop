using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Domain.Entities.Transactions
{
    public class ItemInPurchaseSaleTransaction : BaseEntity
    {
        public Guid PurchaseSaleTransactionId { get; set; }
        public PurchaseSaleTransaction PurchaseSaleTransaction { get; set; }

        public Guid ItemDetailId { get; set; }
        public ItemDetail ItemDetail { get; set; }

        public float ItemPrice { get; set; }
    }
}
