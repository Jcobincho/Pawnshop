using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Domain.Entities.FileStorage
{
    public class PurchaseSaleTransactionAgreement : BaseEntity
    {
        public Guid PurchaseSaleTransactionId { get; set; }
        public PurchaseSaleTransaction PurchaseSaleTransaction { get; set; }

        public string Symbol { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public long TotalBytes { get; set; }
        public string S3Key { get; set; }
    }
}
