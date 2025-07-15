using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto;
using Pawnshop.Application.WorkplacesApplication.Dto;
using Pawnshop.Domain.Enums;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Dto
{
    public class PurchaseSaleTransactionAgreementDto : BaseDto
    {
        public Guid PurchaseSaleTransactionId { get; set; }
        public string Symbol { get; set; }
        public string TypeOfTransaction { get; set; }
        public DateTime TransactionDate { get; set; }
        public ClientDto? Client { get; set; }
        public string Description { get; set; }
        public WorkplaceDto Workplace { get; set; }
        public List<ItemInPurchaseSaleTransactionDto> Items { get; set; }
        public float TotalPrice { get; set; }
    }
}
