using Pawnshop.Domain.Enums;

namespace Pawnshop.Application.ItemHistoriesApplication.Consumers.AddItemHistoryAndItemValuation
{
    public sealed record AddItemHistoryAndItemValuationEvent
    (
        Guid PurchaseSaleTransactionId,
        Guid ItemDetailId,
        float ItemPrice,
        string Description,
        bool AddItemValuation,
        float ItemValuationPrice,
        string Justification
    );
}
