using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemHistoriesApplication.Responses;
using Pawnshop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemHistoriesApplication.Commands.UpdateItemHistory
{
    public sealed class UpdateItemHistoryCommand : BaseCommand<UpdateItemHistoryResponse>
    {
        [Required(ErrorMessage = "Item history ID is required.")]
        public Guid ItemHistoryId { get; set; }

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
