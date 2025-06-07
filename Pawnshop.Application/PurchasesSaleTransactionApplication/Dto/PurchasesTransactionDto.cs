using Pawnshop.Domain.Enums;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Dto
{
    public class PurchasesTransactionDto
    {
        public TypeOfTransactionEnum TypeOfTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}
