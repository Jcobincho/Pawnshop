using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.CompanyEmailsApplication.Commands.AddCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.DeleteCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.UpdateCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Dto;
using Pawnshop.Application.CompanyEmailsApplication.Dto.DtoExtension;
using Pawnshop.Application.CompanyEmailsApplication.Interfaces;
using Pawnshop.Application.CryptographyApplication.Interface;
using Pawnshop.Domain.Entities.CompanyEmail;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.CompanyEmailsInfrastructure.Services
{
    internal sealed class CompanyEmailsService : ICompanyEmailsCommandService, ICompanyEmailsQueryService
    {
        private readonly DbContext _dbContext;
        private readonly ICryptographyService _cryptographyService;

        public CompanyEmailsService(DbContext dbContext, ICryptographyService cryptographyService)
        {
            _dbContext = dbContext;
            _cryptographyService = cryptographyService;
        }

        public async Task<Guid> AddCompanyEmailAsync(AddCompanyEmailCommand command, CancellationToken cancellationToken)
        {
            if(command.IsMainEmail == true)
            {
                await IsAnotherEmailPrimaryStatusAsync(cancellationToken);
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
            var companyEmail = await GetCompanyEmailByIdAsync(command.CompanyEmailId, cancellationToken);

            if(command.IsMainEmail == true)
            {
                await IsAnotherEmailPrimaryStatusAsync(cancellationToken);
            }

            companyEmail.SmtpHost = command.SmtpHost;
            companyEmail.SmtpPort = command.SmtpPort;
            companyEmail.SmtpUser = command.SmtpUser;
            if(!string.IsNullOrEmpty(command.SmtpPassword))
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
            var email = await GetCompanyEmailByIdAsync(command.CompanyEmailId, cancellationToken);

            _dbContext.CompanyEmails.Remove(email);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task IsAnotherEmailPrimaryStatusAsync(CancellationToken cancellationToken)
        {
            var email = await _dbContext.CompanyEmails.FirstOrDefaultAsync(x => x.IsMainEmail == true, cancellationToken);

            if(email != null)
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
