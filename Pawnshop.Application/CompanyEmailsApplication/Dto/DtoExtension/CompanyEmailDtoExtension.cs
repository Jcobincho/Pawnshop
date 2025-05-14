using Microsoft.AspNetCore.Http.HttpResults;
using Pawnshop.Domain.Entities.CompanyEmail;

namespace Pawnshop.Application.CompanyEmailsApplication.Dto.DtoExtension
{
    public static class CompanyEmailDtoExtension
    {
        public static CompanyEmailDto CompanyEmailParseToDto(this CompanyEmail companyEmail)
        {
            return new CompanyEmailDto
            {
                SmtpHost = companyEmail.SmtpHost,
                SmtpPort = companyEmail.SmtpPort,
                SmtpUser = companyEmail.SmtpUser,
                Email = companyEmail.Email,
                IsMainEmail = companyEmail.IsMainEmail,
                CreatedAt = companyEmail.CreatedAt,
                CreatedBy = companyEmail.CreatedBy,
                EditedAt = companyEmail.EditedAt,
                EditedBy = companyEmail.EditedBy,
            };
        }
    }
}
