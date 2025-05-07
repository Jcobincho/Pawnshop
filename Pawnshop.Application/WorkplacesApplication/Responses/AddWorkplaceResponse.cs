namespace Pawnshop.Application.WorkplacesApplication.Responses
{
    public sealed class AddWorkplaceResponse
    {
        public string Message { get; set; } = "Success.";
        public Guid WorkplaceId { get; set; }
    }
}
