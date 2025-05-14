using Pawnshop.Application.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommand:BaseCommand<UpdateCategoryResponse>
    {
        [Required(ErrorMessage ="Category Id is required")]
        public Guid ItemCategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required")]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
