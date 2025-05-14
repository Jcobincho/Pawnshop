using Pawnshop.Application.Base;
using Pawnshop.Application.CompanyEmailsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.CompanyEmailsApplication.Commands.AddCompanyEmail
{
    public sealed class AddCompanyEmailCommand : BaseCommand<AddCompanyEmailResponse>
    {
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
