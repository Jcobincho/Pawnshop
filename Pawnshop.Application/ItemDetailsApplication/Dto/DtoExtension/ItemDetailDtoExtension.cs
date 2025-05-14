using Pawnshop.Application.ItemCategoriesApplication.Dto.DtoExtension;
using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemDetailsApplication.Dto.DtoExtension
{
    public static class ItemDetailDtoExtension
    {
        public static ItemDetailDto ItemDetailParseToDto(this ItemDetail itemDetail)
        {
            return new ItemDetailDto
            {
                ItemDetailId = itemDetail.Id,
                Name = itemDetail.Name,
                ItemCategory = itemDetail.ItemCategory.ItemCategoryParseToDto(),
                Brand = itemDetail.Brand,
                Model = itemDetail.Model,
                SerialNumber = itemDetail.SerialNumber,
                AddedOn = itemDetail.AddedOn,
                Comments = itemDetail.Comments,
                CreatedAt = itemDetail.CreatedAt,
                CreatedBy = itemDetail.CreatedBy,
                EditedAt = itemDetail.EditedAt,
                EditedBy = itemDetail.EditedBy
            };
        }
    }
}
