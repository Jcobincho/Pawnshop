using Pawnshop.Application.ItemHistoriesApplication.Dto;
using Pawnshop.Application.ItemHistoriesApplication.Queries.GetItemHistoriesForItemDetail;
using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemHistoriesApplication.Interfaces
{
    public interface IItemHistoriesQueryService
    {
        Task<ItemHistory> GetItemHistoryByIdAsync(Guid itemHistoryId, CancellationToken cancellationToken);
        Task<List<ItemHistoryForItemDetailDto>> GetItemHistoryForItemDetailAsync(GetItemHistoriesForItemDetailQuery query, CancellationToken cancellationToken);
    }
}
