using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Common.Base;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.AddItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Commands.DeleteItemInPurchaseSaleTransaction;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Queries.GetItemsForPurchaseSaleTransaction;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemInPurchaseSaleSaleTransactionController : BaseController<AddItemInPurchaseSaleTransactionCommand, DeleteItemInPurchaseSaleTransactionCommand, BaseCommand, GetItemsForPurchaseSaleTransactionQuery>
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> PutAsync([FromBody] DeleteItemInPurchaseSaleTransactionCommand data, CancellationToken cancellation)
        {
            return base.PutAsync(data, cancellation);
        }
    }
}
