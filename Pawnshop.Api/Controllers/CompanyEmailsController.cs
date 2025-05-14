using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.CompanyEmailsApplication.Commands.AddCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.DeleteCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Commands.UpdateCompanyEmail;
using Pawnshop.Application.CompanyEmailsApplication.Queries.GetAllComapnyEmails;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class CompanyEmailsController : BaseController<AddCompanyEmailCommand, UpdateCompanyEmailCommand, DeleteCompanyEmailCommand, GetAllComapnyEmailsQuery>
    {
    }
}
