using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemCategoriesApplication.Responses;
using Pawnshop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemHistoriesApplication.Commands.AddItemHistory
{
    public sealed class AddItemHistoryCommand : BaseCommand<AddItemCategoryResponse>
    {
        [Required(ErrorMessage = "Item ID is required.")]
        public Guid ItemDetailId { get; set; }

        [Required(ErrorMessage = "Item status is required.")]
        public ItemStatus ItemStatus { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Workplace is required.")]
        public Guid WorkplaceId { get; set; }

        public DateTime DateFrom { get; set; } = DateTime.Now;
}
    }
