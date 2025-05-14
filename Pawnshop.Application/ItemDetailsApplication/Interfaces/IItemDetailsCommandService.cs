using Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.UpdateItemDetail;

namespace Pawnshop.Application.ItemDetailsApplication.Interfaces
{
    public interface IItemDetailsCommandService
    {
        Task<Guid> AddItemDetailAsync(AddItemDetailsCommand command, CancellationToken cancellationToken);
        Task UpdateItemDetailAsync(UpdateItemDetailCommand command, CancellationToken cancellationToken);
        Task DeleteItemDetailAsync(DeleteItemDetailCommand command, CancellationToken cancellationToken);
    }
}
