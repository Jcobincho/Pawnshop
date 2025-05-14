using Pawnshop.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Dto
{
    public class ItemCategoryDto : BaseDto
    {
        public Guid ItemCategoryId {get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
