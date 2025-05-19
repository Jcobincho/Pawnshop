using Pawnshop.Application.ItemHistoriesApplication.Commands.AddItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Commands.DeleteItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Commands.UpdateItemHistory;

namespace Pawnshop.Application.ItemHistoriesApplication.Interfaces
{
    public interface IItemHistoriesCommandService
    {
        Task<Guid> AddItemHistoryAsync(AddItemHistoryCommand command, CancellationToken cancellationToken);
        Task UpdateItemHistoryAsync(UpdateItemHistoryCommand command, CancellationToken cancellationToken);
        Task DeleteItemHistoryAsync(DeleteItemHistoryCommand command, CancellationToken cancellationToken);
    }
}
