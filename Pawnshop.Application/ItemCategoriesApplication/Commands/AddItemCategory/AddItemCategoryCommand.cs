using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.AddItemCategory
{
    public sealed class AddItemCategoryCommand : BaseCommand<AddItemCategoryResponse>
    {
        [Required(ErrorMessage ="Category name is required.")]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
