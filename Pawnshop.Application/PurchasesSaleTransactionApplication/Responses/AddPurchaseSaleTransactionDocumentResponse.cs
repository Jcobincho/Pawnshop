namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Responses
{
    public sealed class AddPurchaseSaleTransactionDocumentResponse
    {
        public string Message { get; set; } = "Success.";
        public Guid Id { get; set; }
    }
}
