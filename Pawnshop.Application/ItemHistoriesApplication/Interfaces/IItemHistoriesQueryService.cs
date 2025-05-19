using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemHistoriesApplication.Interfaces
{
    public interface IItemHistoriesQueryService
    {
        Task<ItemHistory> GetItemHistoryByIdAsync(Guid itemHistoryId, CancellationToken cancellationToken);
    }
}
