using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemHistoriesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemHistoriesApplication.Commands.DeleteItemHistory
{
    public sealed class DeleteItemHistoryCommand : BaseCommand<DeleteItemHistoryResponse>
    {
        [Required(ErrorMessage = "Item hisotry ID is required.")]
        public Guid ItemHistoryId { get; set; }
    }
}
