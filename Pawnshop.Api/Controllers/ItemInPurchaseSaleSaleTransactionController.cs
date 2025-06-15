using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemInPurchaseSaleSaleTransactionController : BaseController<AddItemInPurchaseSaleTransactionCommand, BaseCommand, BaseCommand, BaseQuery >
    {
    }
}
