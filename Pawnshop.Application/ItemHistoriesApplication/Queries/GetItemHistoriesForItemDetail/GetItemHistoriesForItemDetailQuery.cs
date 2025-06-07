using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemHistoriesApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemHistoriesApplication.Queries.GetItemHistoriesForItemDetail
{
    public sealed class GetItemHistoriesForItemDetailQuery : BaseQuery<GetItemHistoriesForItemDetailResponse>
    {
        [Required(ErrorMessage = "Item detail ID is required.")]
        public Guid ItemDetailId { get; set; }
    }
}
