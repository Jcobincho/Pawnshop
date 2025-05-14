using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemDetailsApplication.Interfaces
{
    public interface IItemDetailsQueryService
    {
        Task<ItemDetail> GetItemDetailByIdAsync(Guid itemDetailId, CancellationToken cancellationToken);
    }
}
