namespace Pawnshop.Domain.Entities.CompanyEmail
{
    public class CompanyEmail : BaseEntity
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string Email { get; set; }
        public bool IsMainEmail { get; set; }
    }
}
