namespace Pawnshop.Application.CompanyEmailsApplication.Responses
{
    public sealed class AddCompanyEmailResponse
    {
        public string Message { get; set; } = "Success.";
        public Guid CompanyEmailId { get; set; }
    }
}
