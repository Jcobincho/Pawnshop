using Pawnshop.Application.ItemDetailsApplication.Dto;
using Pawnshop.Domain.Entities.Item;

namespace Pawnshop.Application.ItemDetailsApplication.Interfaces
{
    public interface IItemDetailsQueryService
    {
        Task<ItemDetail> GetItemDetailByIdAsync(Guid itemDetailId, CancellationToken cancellationToken);
        Task<List<ItemDetailDto>> GetAllItemDetailsAsDtoAsync(CancellationToken cancellationToken);
        Task<bool> ItemDetailExistsAsync(Guid itemDetailId, CancellationToken cancellationToken);
    }
}
