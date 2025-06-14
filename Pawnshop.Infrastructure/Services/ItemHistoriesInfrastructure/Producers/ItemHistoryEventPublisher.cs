using MassTransit;
using Pawnshop.Application.ItemHistoriesApplication.Consumers.AddItemHistoryAndItemValuation;
using Pawnshop.Application.ItemHistoriesApplication.Producers;

namespace Pawnshop.Infrastructure.Services.ItemHistoriesInfrastructure.Producers
{
    internal sealed class ItemHistoryEventPublisher : IItemHistoryEventPublisher
    {
        private readonly IPublishEndpoint _publisherEndpoint;

        public ItemHistoryEventPublisher(IPublishEndpoint publisherEndpoint)
        {
            _publisherEndpoint = publisherEndpoint;
        }

        public async Task AddItemHistoryAndItemValuationPublishAsync(AddItemHistoryAndItemValuationEvent @event, CancellationToken cancellationToken)
        {
            await _publisherEndpoint.Publish(@event, cancellationToken);
        }
    }
}
