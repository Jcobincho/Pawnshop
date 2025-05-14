using Pawnshop.Application.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommand : BaseCommand<DeleteCategoryResponse>
    {
        [Required(ErrorMessage ="Category id is required")]
        public Guid CategoryId { get; set; }
    }
}
