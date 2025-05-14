using Pawnshop.Application.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteItemCategory
{
    public sealed class DeleteItemCategoryCommand : BaseCommand<DeleteItemCategoryResponse>
    {
        [Required(ErrorMessage ="Category ID is required.")]
        public Guid CategoryId { get; set; }
    }
}
