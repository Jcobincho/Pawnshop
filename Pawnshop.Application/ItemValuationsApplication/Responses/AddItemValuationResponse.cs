namespace Pawnshop.Application.ItemValuationsApplication.Responses
{
    public sealed class AddItemValuationResponse
    {
        public string Message { get; set; } = "Success.";
        public Guid Id { get; set; }
    }
}
