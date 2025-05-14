using Pawnshop.Application.CompanyEmailsApplication.Dto;

namespace Pawnshop.Application.CompanyEmailsApplication.Responses
{
    public sealed class GetAllComapnyEmailsResponse
    {
        public List<CompanyEmailDto> AllCompanyEmailsList { get; set; }
    }
}
