using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System.ComponentModel.DataAnnotations;


namespace Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateItemCategory
{
    public sealed class UpdateItemCategoryCommand:BaseCommand<UpdateItemCategoryResponse>
    {
        [Required(ErrorMessage ="Category ID is required.")]
        public Guid ItemCategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required.")]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
