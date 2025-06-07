using Pawnshop.Application.Common.Base;
using Pawnshop.Application.CompanyEmailsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.CompanyEmailsApplication.Commands.UpdateCompanyEmail
{
    public sealed class UpdateCompanyEmailCommand : BaseCommand<UpdateCompanyEmailResponse>
    {
        [Required(ErrorMessage = "Company E-mail ID is required.")]
        public Guid CompanyEmailId { get; set; }

        [Required(ErrorMessage = "SMTP host is required.")]
        public string SmtpHost { get; set; }

        [Required(ErrorMessage = "SMTP port is required.")]
        public int SmtpPort { get; set; }

        [Required(ErrorMessage = "SMTP user is required.")]
        public string SmtpUser { get; set; }

        [Required(ErrorMessage = "SMTP password is required.")]
        public string SmtpPassword { get; set; }

        [Required(ErrorMessage = "E-mail is required.")]
        public string Email { get; set; }

        public bool IsMainEmail { get; set; } = false;
    }
}
