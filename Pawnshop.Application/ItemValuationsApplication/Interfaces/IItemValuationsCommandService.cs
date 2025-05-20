using Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.DeleteItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.UpdateItemValuation;

namespace Pawnshop.Application.ItemValuationsApplication.Interfaces
{
    public interface IItemValuationsCommandService
    {
        Task<Guid> AddItemValuationAsync(AddItemValuationCommand command, CancellationToken cancellationToken);
        Task UpdateItemValuationAsync(UpdateItemValuationCommand command, CancellationToken cancellationToken);
        Task DeleteItemValuationAsync(DeleteItemValuationCommand command, CancellationToken cancellationToken);
    }
}
