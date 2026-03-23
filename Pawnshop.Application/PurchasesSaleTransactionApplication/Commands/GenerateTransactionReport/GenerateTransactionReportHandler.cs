using MediatR;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Consumers.GenerateTransactionReport;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Producers;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.GenerateTransactionReport;

public sealed class GenerateTransactionReportHandler : IRequestHandler<GenerateTransactionReportCommand>
{
    private readonly IPurchaseSaleTransactionEventPublisher _eventPublisher;

    public GenerateTransactionReportHandler(IPurchaseSaleTransactionEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task Handle(GenerateTransactionReportCommand request, CancellationToken cancellationToken)
    {
        var @event = new GenerateTransactionReportEvent(request.TransactionId, request.UserIdFromClaims.ToString());
        await _eventPublisher.GenerateTransactionReportPublishAsync(@event, cancellationToken);
    }
}
