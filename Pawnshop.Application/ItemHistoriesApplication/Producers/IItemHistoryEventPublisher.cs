using Pawnshop.Application.ItemHistoriesApplication.Consumers.AddItemHistoryAndItemValuation;

namespace Pawnshop.Application.ItemHistoriesApplication.Producers
{
    public interface IItemHistoryEventPublisher
    {
        Task AddItemHistoryAndItemValuationPublishAsync(AddItemHistoryAndItemValuationEvent @event, CancellationToken cancellationToken);
    }
}
