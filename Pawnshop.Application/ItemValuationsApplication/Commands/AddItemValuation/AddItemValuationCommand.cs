using Pawnshop.Application.Base;
using Pawnshop.Application.ItemValuationsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation
{
    public sealed class AddItemValuationCommand : BaseCommand<AddItemValuationResponse>
    {
        [Required(ErrorMessage = "Item history ID is required.")]
        public Guid ItemHistoryId { get; set; }

        public DateTime ValuationOnDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "PRice is required.")]
        public float Price { get; set; }

        public string Justification { get; set; } = string.Empty;
    }
}
