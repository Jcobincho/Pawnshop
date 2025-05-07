using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace;
using Pawnshop.Application.WorkplacesApplication.Queries.GetAllWorkPlaces;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class WorkplacesController : BaseController<AddWorkplaceCommand, UpdateWorkplaceCommand, DeleteWorkplaceCommand, GetAllWorkplacesQuery>
    {
    }
}
