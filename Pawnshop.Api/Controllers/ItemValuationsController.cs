using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.ItemValuationsApplication.Commands.AddItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.DeleteItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Commands.UpdateItemValuation;
using Pawnshop.Application.ItemValuationsApplication.Queries.GetItemValuationForItemHistory;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ItemValuationsController : BaseController<AddItemValuationCommand, UpdateItemValuationCommand, DeleteItemValuationCommand, BaseQuery>
    {
        [HttpGet("get-item-valuations-for-item-history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetItemHistoryForItemDetailAsync([FromQuery] GetItemValuationForItemHistoryQuery query, CancellationToken cancellationToken)
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
