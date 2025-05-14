using MediatR;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CompanyEmailsApplication.Responses;

namespace Pawnshop.Application.CompanyEmailsApplication.Commands.AddCompanyEmail
{
    public sealed class AddCompanyEmailHandler : IRequestHandler<AddCompanyEmailCommand, AddCompanyEmailResponse>
    {
        private readonly ICompanyEmailsCommandService _companyEmailsCommandService;

        public AddCompanyEmailHandler(ICompanyEmailsCommandService companyEmailsCommandService)
        {
            _companyEmailsCommandService = companyEmailsCommandService;
        }

        public async Task<AddCompanyEmailResponse> Handle(AddCompanyEmailCommand request, CancellationToken cancellationToken)
        {
            var id = await _companyEmailsCommandService.AddCompanyEmailAsync(request, cancellationToken);

            return new AddCompanyEmailResponse
            {
                CompanyEmailId = id
            };
        }
    }
}
