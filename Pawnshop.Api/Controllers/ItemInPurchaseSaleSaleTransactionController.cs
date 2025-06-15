using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.DeleteItemInPurchaseSaleTransaction;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemInPurchaseSaleSaleTransactionController : BaseController<AddItemInPurchaseSaleTransactionCommand, DeleteItemInPurchaseSaleTransactionCommand, BaseCommand, BaseQuery >
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> PutAsync([FromBody] DeleteItemInPurchaseSaleTransactionCommand data, CancellationToken cancellation)
        {
            return base.PutAsync(data, cancellation);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> GetAsync([FromQuery] BaseQuery data, CancellationToken cancellation)
        {
            return base.GetAsync(data, cancellation);
        }
    }
}
