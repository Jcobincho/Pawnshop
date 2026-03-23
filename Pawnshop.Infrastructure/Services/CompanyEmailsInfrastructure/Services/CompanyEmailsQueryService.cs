using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.CompanyEmailsApplication.Dto;
using Pawnshop.Application.CompanyEmailsApplication.Dto.DtoExtension;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Domain.Entities.CompanyEmail;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.CompanyEmailsInfrastructure.Services
{
    internal sealed class CompanyEmailsQueryService : ICompanyEmailsQueryService
    {
        private readonly DbContext _dbContext;

        public CompanyEmailsQueryService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task IsAnotherEmailPrimaryStatusAsync(CancellationToken cancellationToken)
        {
            var email = await _dbContext.CompanyEmails.FirstOrDefaultAsync(x => x.IsMainEmail == true, cancellationToken);

            if (email != null)
            {
                throw new BadRequestException($"You can't add another main email. Currently, this status is held by the email {email.Email}");
            }
        }

        public async Task<CompanyEmail> GetCompanyEmailByIdAsync(Guid companyEmailId, CancellationToken cancellationToken)
        {
            var companyEmail = await _dbContext.CompanyEmails.FindAsync(companyEmailId, cancellationToken);

            if (companyEmail == null)
            {
                throw new BadRequestException("Company E-mail doesn't exist.");
            }

            return companyEmail;
        }

        public async Task<List<CompanyEmailDto>> GetAllCompanyEmailsAsDtoAsync(CancellationToken cancellationToken)
        {
            var companyEmails = await _dbContext.CompanyEmails.Select(x => x.CompanyEmailParseToDto()).ToListAsync();

            return companyEmails;
        }
    }
}
