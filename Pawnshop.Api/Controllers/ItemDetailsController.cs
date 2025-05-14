using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.UpdateItemDetail;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemDetailsController : BaseController<AddItemDetailsCommand, UpdateItemDetailCommand, DeleteItemDetailCommand, BaseQuery>
    {
    }
}
