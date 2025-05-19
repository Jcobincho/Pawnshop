using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.ItemHistoriesApplication.Commands.AddItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Commands.DeleteItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Commands.UpdateItemHistory;
using Pawnshop.Application.ItemHistoriesApplication.Queries.GetItemHistoriesForItemDetail;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemHistoriesController : BaseController<AddItemHistoryCommand, UpdateItemHistoryCommand, DeleteItemHistoryCommand, BaseQuery>
    {
        [HttpGet("get-item-history-for-item-detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItemHistoryForItemDetailAsync([FromQuery] GetItemHistoriesForItemDetailQuery query, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> GetAsync([FromQuery] BaseQuery data, CancellationToken cancellation)
        {
            return base.GetAsync(data, cancellation);
        }
    }
}
