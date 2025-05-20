using Pawnshop.Application.Base;
using Pawnshop.Application.ItemValuationsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemValuationsApplication.Commands.UpdateItemValuation
{
    public sealed class UpdateItemValuationCommand : BaseCommand<UpdateItemValuationResponse>
    {
        [Required(ErrorMessage = "Item valuation ID is required")]
        public Guid ItemValuationId { get; set; }

        [Required(ErrorMessage = "Item history ID is required.")]
        public Guid ItemHistoryId { get; set; }

        public DateTime ValuationOnDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "PRice is required.")]
        public float Price { get; set; }

        public string Justification { get; set; } = string.Empty;
    }
}
