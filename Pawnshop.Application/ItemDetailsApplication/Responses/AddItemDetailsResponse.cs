namespace Pawnshop.Application.ItemDetailsApplication.Responses
{
    public sealed class AddItemDetailsResponse
    {
        public string Message { get; set; } = "Success.";
        public Guid Id { get; set; }
    }
}
