using Pawnshop.Application.ItemValuationsApplication.Dto;
using Pawnshop.Application.ItemValuationsApplication.Queries.GetItemValuationForItemHistory;
using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemValuationsApplication.Interfaces
{
    public interface IItemValuationsQueryService
    {
        Task<ItemValuation> GetItemValuationByIdAsync(Guid itemValuationId, CancellationToken cancellationToken);
        Task<List<ItemValuationForItemHistoryDto>> GetItemValuationForItemHistoryAsync(GetItemValuationForItemHistoryQuery query, CancellationToken cancellationToken);
    }
}
