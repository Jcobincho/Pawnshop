using MediatR;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CompanyEmailsApplication.Responses;

namespace Pawnshop.Application.CompanyEmailsApplication.Commands.UpdateCompanyEmail
{
    public sealed class UpdateCompanyEmailHandler : IRequestHandler<UpdateCompanyEmailCommand, UpdateCompanyEmailResponse>
    {
        private readonly ICompanyEmailsCommandService _companyEmailsCommandService;

        public UpdateCompanyEmailHandler(ICompanyEmailsCommandService companyEmailsCommandService)
        {
            _companyEmailsCommandService = companyEmailsCommandService;
        }

        public async Task<UpdateCompanyEmailResponse> Handle(UpdateCompanyEmailCommand request, CancellationToken cancellationToken)
        {
            await _companyEmailsCommandService.UpdateCompanyEmailAsync(request, cancellationToken);

            return new UpdateCompanyEmailResponse();
        }
    }
}
