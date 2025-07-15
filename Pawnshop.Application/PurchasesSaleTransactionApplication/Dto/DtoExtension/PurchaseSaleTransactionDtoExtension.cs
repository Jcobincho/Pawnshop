using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.ItemCategoriesApplication.Dto;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto;
using Pawnshop.Application.WorkplacesApplication.Dto;
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
                Symbol = purchaseSaleTransaction.Symbol,
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
                Symbol = purchaseSaleTransaction.Symbol,
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

        public static PurchaseSaleTransactionAgreementDto PurchaseSaleTransactionAgreementParseToDto(this PurchaseSaleTransaction purchaseSaleTransaction)
        {
            return new PurchaseSaleTransactionAgreementDto
            {
                PurchaseSaleTransactionId = purchaseSaleTransaction.Id,
                Symbol = purchaseSaleTransaction.Symbol,
                TypeOfTransaction = purchaseSaleTransaction.TypeOfTransaction.ToString(),
                TransactionDate = purchaseSaleTransaction.TransactionDate,
                Client = purchaseSaleTransaction.Client != null ? new ClientDto
                {
                    ClientId = purchaseSaleTransaction.Client.Id,
                    Name = purchaseSaleTransaction.Client.Name,
                    SecondName = purchaseSaleTransaction.Client.SecondName,
                    Surname = purchaseSaleTransaction.Client.Surname,
                    BirthDate = purchaseSaleTransaction.Client.BirthDate,
                    Pesel = purchaseSaleTransaction.Client.Pesel,
                    IdCardNumber = purchaseSaleTransaction.Client.IdCardNumber,
                    TelephoneNumber = purchaseSaleTransaction.Client.TelephoneNumber,
                    Email = purchaseSaleTransaction.Client.Email,
                    Description = purchaseSaleTransaction.Client.Description
                } : null,
                Description = purchaseSaleTransaction.Description,
                Workplace = new WorkplaceDto
                {
                    WorkplaceId = purchaseSaleTransaction.Workplace.Id,
                    Country = purchaseSaleTransaction.Workplace.Country,
                    Region = purchaseSaleTransaction.Workplace.Region,
                    StreetAndBuildingNumber = purchaseSaleTransaction.Workplace.StreetAndBuildingNumber,
                    ZipCode = purchaseSaleTransaction.Workplace.ZipCode,
                    City = purchaseSaleTransaction.Workplace.City
                },
                Items = purchaseSaleTransaction.ItemsInPurchaseSaleTransaction?.Select(item => new ItemInPurchaseSaleTransactionDto
                {
                    ItemInPurchaseSaleTransactionId = item.Id,
                    ItemDetailId = item.ItemDetailId,
                    Name = item.ItemDetail?.Name,
                    ItemCategory = item.ItemDetail?.ItemCategory != null ? new ItemCategoryDto
                    {
                        Name = item.ItemDetail.ItemCategory.Name,
                        Description = item.ItemDetail.ItemCategory.Description
                    } : null,
                    Brand = item.ItemDetail?.Brand,
                    Model = item.ItemDetail?.Model,
                    SerialNumber = item.ItemDetail?.SerialNumber,
                    AddedOn = item.ItemDetail?.AddedOn ?? default,
                    Comments = item.ItemDetail?.Comments,
                    Price = item.ItemPrice
                }).ToList() ?? new List<ItemInPurchaseSaleTransactionDto>(),
                TotalPrice = purchaseSaleTransaction.ItemsInPurchaseSaleTransaction?.Sum(item => item.ItemPrice) ?? 0
            };
        }
    }
}
