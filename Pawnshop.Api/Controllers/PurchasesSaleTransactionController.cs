using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PurchasesSaleTransactionController : BaseController<AddPurchaseSaleTransactionDocumentCommand, UpdatePurchaseSaleTransactionDocumentCommand, DeletePurchaseSaleTransactionDocumentCommand, GetEverySalesTransactionQuery>
    {
        /// <summary>
        /// Get every purchases documents for specific client
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("get-purchases-for-specific-client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPurchasesForSpecificClientAsync([FromQuery] GetPurchasesForClientQuery data, CancellationToken cancellation)
        {
            var response = await Sender.Send(data, cancellation);

            return Ok(response);
        }
    }
}