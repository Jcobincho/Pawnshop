using Pawnshop.Application.Base;
using Pawnshop.Application.CompanyEmailsApplication.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.CompanyEmailsApplication.Commands.DeleteCompanyEmail
{
    public sealed class DeleteCompanyEmailCommand : BaseCommand<DeleteCompanyEmailResponse>
    {
        [Required(ErrorMessage = "Company E-mail Id is required.")]
        public Guid CompanyEmailId { get; set; }
    }
}
