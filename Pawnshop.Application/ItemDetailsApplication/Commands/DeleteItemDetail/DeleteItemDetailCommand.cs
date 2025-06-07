using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemDetailsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail
{
    public sealed class DeleteItemDetailCommand : BaseCommand<DeleteItemDetailResponse>
    {
        [Required(ErrorMessage = "Item ID is required.")]
        public Guid ItemId { get; set; }
    }
}
