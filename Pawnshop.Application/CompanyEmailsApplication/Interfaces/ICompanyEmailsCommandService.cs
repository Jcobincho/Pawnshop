using Pawnshop.Application.CompanyEmailsApplication.Commands.AddCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.DeleteCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.UpdateCompanyEmail;

namespace Pawnshop.Application.CompanyEmailsApplication.Interfaces
{
    public interface ICompanyEmailsCommandService
    {
        Task<Guid> AddCompanyEmailAsync(AddCompanyEmailCommand command, CancellationToken cancellationToken);
        Task UpdateCompanyEmailAsync(UpdateCompanyEmailCommand command, CancellationToken cancellationToken);
        Task DeleteCompanyEmailAsync(DeleteCompanyEmailCommand command, CancellationToken cancellationToken);
    }
}
