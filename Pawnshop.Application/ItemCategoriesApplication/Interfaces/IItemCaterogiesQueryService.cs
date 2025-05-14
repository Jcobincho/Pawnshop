using Pawnshop.Application.ItemCategoriesApplication.Dto;
using Pawnshop.Domain.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Interfaces
{
    public interface IItemCategoriesQueryService
    {
        Task<ItemCategory> GetItemCategoryByIdAsync(Guid itemCategoryId, CancellationToken cancellationToken);
        Task<List<ItemCategoryDto>> GetAllItemCategoriesAsync(CancellationToken cancellationToken);
    }
}
