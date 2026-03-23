using Pawnshop.Application.CompanyEmailsApplication.Commands.AddCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.DeleteCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.UpdateCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CryptographyApplication.Interface;
using Pawnshop.Domain.Entities.CompanyEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.CompanyEmailsInfrastructure.Services
{
    internal sealed class CompanyEmailsCommandService : ICompanyEmailsCommandService
    {
        private readonly DbContext _dbContext;
        private readonly ICompanyEmailsQueryService _companyEmailsQueryService;
        private readonly ICryptographyService _cryptographyService;

        public CompanyEmailsCommandService(DbContext dbContext, ICompanyEmailsQueryService companyEmailsQueryService, ICryptographyService cryptographyService)
        {
            _dbContext = dbContext;
            _companyEmailsQueryService = companyEmailsQueryService;
            _cryptographyService = cryptographyService;
        }

        public async Task<Guid> AddCompanyEmailAsync(AddCompanyEmailCommand command, CancellationToken cancellationToken)
        {
            if (command.IsMainEmail == true)
            {
                await _companyEmailsQueryService.IsAnotherEmailPrimaryStatusAsync(cancellationToken);
            }

            string encryptedPassword = _cryptographyService.Encrypt(command.SmtpPassword);

            var newEmail = new CompanyEmail
            {
                SmtpHost = command.Email,
                SmtpPort = command.SmtpPort,
                SmtpUser = command.SmtpUser,
                SmtpPassword = encryptedPassword,
                Email = command.Email,
                IsMainEmail = command.IsMainEmail
            };

            await _dbContext.CompanyEmails.AddAsync(newEmail, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newEmail.Id;
        }

        public async Task UpdateCompanyEmailAsync(UpdateCompanyEmailCommand command, CancellationToken cancellationToken)
        {
            var companyEmail = await _companyEmailsQueryService.GetCompanyEmailByIdAsync(command.CompanyEmailId, cancellationToken);

            if (command.IsMainEmail == true)
            {
                await _companyEmailsQueryService.IsAnotherEmailPrimaryStatusAsync(cancellationToken);
            }

            companyEmail.SmtpHost = command.SmtpHost;
            companyEmail.SmtpPort = command.SmtpPort;
            companyEmail.SmtpUser = command.SmtpUser;
            if (!string.IsNullOrEmpty(command.SmtpPassword))
            {
                string encryptedPassword = _cryptographyService.Encrypt(command.SmtpPassword);
                companyEmail.SmtpPassword = encryptedPassword;
            }
            companyEmail.Email = command.Email;
            companyEmail.IsMainEmail = command.IsMainEmail;

            _dbContext.CompanyEmails.Update(companyEmail);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteCompanyEmailAsync(DeleteCompanyEmailCommand command, CancellationToken cancellationToken)
        {
            var email = await _companyEmailsQueryService.GetCompanyEmailByIdAsync(command.CompanyEmailId, cancellationToken);

            _dbContext.CompanyEmails.Remove(email);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
