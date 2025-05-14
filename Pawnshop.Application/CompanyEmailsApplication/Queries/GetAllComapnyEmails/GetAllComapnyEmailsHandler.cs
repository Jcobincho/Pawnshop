using MediatR;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CompanyEmailsApplication.Responses;

namespace Pawnshop.Application.CompanyEmailsApplication.Queries.GetAllComapnyEmails
{
    public sealed class GetAllComapnyEmailsHandler : IRequestHandler<GetAllComapnyEmailsQuery, GetAllComapnyEmailsResponse>
    {
        private readonly ICompanyEmailsQueryService _companyEmailsQueryService;

        public GetAllComapnyEmailsHandler(ICompanyEmailsQueryService companyEmailsQueryService)
        {
            _companyEmailsQueryService = companyEmailsQueryService;
        }

        public async Task<GetAllComapnyEmailsResponse> Handle(GetAllComapnyEmailsQuery request, CancellationToken cancellationToken)
        {
            var companyEmails = await _companyEmailsQueryService.GetAllCompanyEmailsAsDtoAsync(cancellationToken);

            return new GetAllComapnyEmailsResponse
            {
                AllCompanyEmailsList = companyEmails,
            };
        }
    }
}
