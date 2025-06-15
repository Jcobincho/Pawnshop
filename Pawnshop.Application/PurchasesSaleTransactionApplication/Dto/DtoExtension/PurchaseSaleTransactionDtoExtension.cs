using Pawnshop.Application.WorkplacesApplication.Dto.DtoExtension;
using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Dto.DtoExtension
{
    public static class PurchaseSaleTransactionDtoExtension
    {
        public static SalesTransactionDto SalesTransactionPraseToDto(this PurchaseSaleTransaction purchaseSaleTransaction)
        {
            return new SalesTransactionDto
            {
                TypeOfTransaction = purchaseSaleTransaction.TypeOfTransaction,
                TransactionDate = purchaseSaleTransaction.TransactionDate,
                Description = purchaseSaleTransaction.Description,
                Workplace = purchaseSaleTransaction.Workplace.WorkplaceParseToDto(),
                CreatedAt = purchaseSaleTransaction.CreatedAt,
                CreatedBy = purchaseSaleTransaction.CreatedBy,
                EditedAt = purchaseSaleTransaction.EditedAt,
                EditedBy = purchaseSaleTransaction.EditedBy
            };
        }

        public static PurchasesTransactionDto PurchasesTransactionPraseToDto(this PurchaseSaleTransaction purchaseSaleTransaction)
        {
            return new PurchasesTransactionDto
            {
                TypeOfTransaction = purchaseSaleTransaction.TypeOfTransaction,
                TransactionDate = purchaseSaleTransaction.TransactionDate,
                Description = purchaseSaleTransaction.Description,
                Workplace = purchaseSaleTransaction.Workplace.WorkplaceParseToDto(),
                CreatedAt = purchaseSaleTransaction.CreatedAt,
                CreatedBy = purchaseSaleTransaction.CreatedBy,
                EditedAt = purchaseSaleTransaction.EditedAt,
                EditedBy = purchaseSaleTransaction.EditedBy
            };
        }
    }
}
