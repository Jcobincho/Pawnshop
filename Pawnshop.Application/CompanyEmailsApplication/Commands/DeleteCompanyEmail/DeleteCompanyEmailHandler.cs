using MediatR;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CompanyEmailsApplication.Responses;

namespace Pawnshop.Application.CompanyEmailsApplication.Commands.DeleteCompanyEmail
{
    public sealed class DeleteCompanyEmailHandler : IRequestHandler<DeleteCompanyEmailCommand, DeleteCompanyEmailResponse>
    {
        private readonly ICompanyEmailsCommandService _companyEmailsCommandService;

        public DeleteCompanyEmailHandler(ICompanyEmailsCommandService companyEmailsCommandService)
        {
            _companyEmailsCommandService = companyEmailsCommandService;
        }

        public async Task<DeleteCompanyEmailResponse> Handle(DeleteCompanyEmailCommand request, CancellationToken cancellationToken)
        {
            await _companyEmailsCommandService.DeleteCompanyEmailAsync(request, cancellationToken);

            return new DeleteCompanyEmailResponse();
        }
    }
}
