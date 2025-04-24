using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.ClientsApplication.Commands.AddClient;
using Pawnshop.Application.ClientsApplication.Commands.DeleteClient;
using Pawnshop.Application.ClientsApplication.Commands.UpdateClient;
using Pawnshop.Application.ClientsApplication.Queries.GetAllClients;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ClientsController : BaseController<AddClientCommand, UpdateClientCommand, DeleteClientCommand, GetAllClientsQuery>
    {

    }
}
