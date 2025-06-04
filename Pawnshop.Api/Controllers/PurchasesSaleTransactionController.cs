using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Base;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PurchasesSaleTransactionController : BaseController<AddPurchaseSaleTransactionDocumentCommand, UpdatePurchaseSaleTransactionDocumentCommand, DeletePurchaseSaleTransactionDocumentCommand, BaseQuery>
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public override Task<IActionResult> GetAsync([FromQuery] BaseQuery data, CancellationToken cancellationToken)
        {
            return base.GetAsync(data, cancellationToken);
        }
    }
}
