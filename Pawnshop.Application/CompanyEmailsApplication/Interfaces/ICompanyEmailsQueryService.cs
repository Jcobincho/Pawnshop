using Pawnshop.Application.CompanyEmailsApplication.Dto;
using Pawnshop.Domain.Entities.CompanyEmail;

namespace Pawnshop.Application.CompanyEmailsApplication.Interfaces
{
    public interface ICompanyEmailsQueryService
    {
        Task IsAnotherEmailPrimaryStatusAsync(CancellationToken cancellationToken);
        Task<CompanyEmail> GetCompanyEmailByIdAsync(Guid companyEmailId, CancellationToken cancellationToken);
        Task<List<CompanyEmailDto>> GetAllCompanyEmailsAsDtoAsync(CancellationToken cancellationToken);
    }
}
