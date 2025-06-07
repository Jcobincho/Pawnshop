using Pawnshop.Application.Common.Base;

namespace Pawnshop.Application.CompanyEmailsApplication.Dto
{
    public class CompanyEmailDto : BaseDto
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string Email { get; set; }
        public bool IsMainEmail { get; set; }
    }
}
