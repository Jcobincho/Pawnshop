﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.UpdateItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Quersies.GetAllItemDetails;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemDetailsController : BaseController<AddItemDetailsCommand, UpdateItemDetailCommand, DeleteItemDetailCommand, GetAllItemDetailsQuery>
    {
    }
}
