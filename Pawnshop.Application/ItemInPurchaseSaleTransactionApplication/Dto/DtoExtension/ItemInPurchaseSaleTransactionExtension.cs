using Pawnshop.Application.ItemCategoriesApplication.Dto;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Entities.Transactions;
using System.Xml.Linq;

namespace Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Dto.DtoExtension
{
    public static class ItemInPurchaseSaleTransactionExtension
    {
        public static ItemInPurchaseSaleTransactionDto ItemInPurchaseSaleTransactionParseToDto(this ItemInPurchaseSaleTransaction item)
        {
            return new ItemInPurchaseSaleTransactionDto
            {
                ItemInPurchaseSaleTransactionId = item.Id,
                ItemDetailId = item.ItemDetailId,
                Name = item.ItemDetail?.Name,
                ItemCategory = item.ItemDetail?.ItemCategory != null
                    ? new ItemCategoryDto
                    {
                        Name = item.ItemDetail.ItemCategory.Name,
                        Description = item.ItemDetail.ItemCategory.Description
                    }
                    : new ItemCategoryDto(),
                Brand = item.ItemDetail?.Brand,
                Model = item.ItemDetail?.Model,
                SerialNumber = item.ItemDetail?.SerialNumber,
                AddedOn = item.ItemDetail?.AddedOn ?? default,
                Comments = item.ItemDetail?.Comments,
                Price = item.ItemPrice
            };
        }
    }
}
