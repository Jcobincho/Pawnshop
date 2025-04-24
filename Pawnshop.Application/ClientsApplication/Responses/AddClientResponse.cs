namespace Pawnshop.Application.ClientsApplication.Responses
{
    public sealed class AddClientResponse
    {
        public Guid ClientId { get; set; }
        public string Message { get; set; } = "Success.";        
    }
}
