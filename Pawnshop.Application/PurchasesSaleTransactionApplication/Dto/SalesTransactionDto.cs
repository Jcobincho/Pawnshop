using Pawnshop.Application.Common.Base;
using Pawnshop.Application.WorkplacesApplication.Dto;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Dto
{
    public class SalesTransactionDto : BaseDto
    {
        public TypeOfTransactionEnum TypeOfTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public WorkplaceDto Workplace { get; set; }
    }
}
