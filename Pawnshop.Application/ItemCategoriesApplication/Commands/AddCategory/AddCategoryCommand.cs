using Pawnshop.Application.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.AddCategory
{
    public sealed class AddCategoryCommand : BaseCommand<AddCategoryResponse>
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
