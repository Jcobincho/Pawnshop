using Pawnshop.Application.Base;
using Pawnshop.Application.ItemValuationsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemValuationsApplication.Commands.DeleteItemValuation
{
    public sealed class DeleteItemValuationCommand : BaseCommand<DeleteItemValuationResponse>
    {
        [Required(ErrorMessage = "Item valuation ID is required.")]
        public Guid ItemValuationId { get; set; }
    }
}
