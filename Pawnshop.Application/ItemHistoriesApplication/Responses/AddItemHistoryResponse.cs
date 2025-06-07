namespace Pawnshop.Application.ItemHistoriesApplication.Responses
{
    public sealed class AddItemHistoryResponse
    {
        public string Message { get; set; } = "Success.";
        public Guid Id { get; set; }
    }
}
