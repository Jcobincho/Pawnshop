using Pawnshop.Application.ItemCategoriesApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Responses
{
    public sealed class GetAllItemCategoriesResponse
    {
        public List<ItemCategoryDto> AllItemCategoriesList {get; set;}
    }
}
