using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Domain.Entities.Transactions
{
    public class PurchaseSaleTransaction : BaseEntity
    {
        public string Symbol { get; set; }
        public TypeOfTransactionEnum TypeOfTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }
        public string Description { get; set; }
        public Guid WorkplaceId { get; set; }
        public Workplace Workplace { get; set; }

        public List<ItemInPurchaseSaleTransaction> ItemsInPurchaseSaleTransaction { get; set; } = new List<ItemInPurchaseSaleTransaction>();
    }

    public class PurchaseSaleTransactionrConfiguration : IEntityTypeConfiguration<PurchaseSaleTransaction>
    {
        public void Configure(EntityTypeBuilder<PurchaseSaleTransaction> builder)
        {
            builder.HasOne(u => u.Client)
                   .WithMany()
                   .HasForeignKey(u => u.ClientId)
                   .IsRequired(false);

            builder.HasMany(t => t.ItemsInPurchaseSaleTransaction)
                   .WithOne(i => i.PurchaseSaleTransaction)
                   .HasForeignKey(i => i.PurchaseSaleTransactionId)
                   .IsRequired();
        }
    }
}
