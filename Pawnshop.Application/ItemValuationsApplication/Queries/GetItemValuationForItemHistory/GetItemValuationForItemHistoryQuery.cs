using Pawnshop.Application.Base;
using Pawnshop.Application.ItemValuationsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemValuationsApplication.Queries.GetItemValuationForItemHistory
{
    public sealed class GetItemValuationForItemHistoryQuery : BaseQuery<GetItemValuationForItemHistoryResponse>
    {
        [Required(ErrorMessage = "Item history ID is required.")]
        public Guid ItemHistoryId { get; set; }
    }
}
