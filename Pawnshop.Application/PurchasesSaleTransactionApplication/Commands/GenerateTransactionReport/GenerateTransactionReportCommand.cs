using Pawnshop.Application.Common.Base;
using MediatR;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.GenerateTransactionReport;

public sealed class GenerateTransactionReportCommand(Guid transactionId) : BaseCommand, IRequest
{
    public Guid TransactionId { get; } = transactionId;
}
