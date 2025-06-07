using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.Common.Base;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction;

namespace Pawnshop.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class PurchasesSaleTransactionController : BaseController<AddPurchaseSaleTransactionDocumentCommand, UpdatePurchaseSaleTransactionDocumentCommand, DeletePurchaseSaleTransactionDocumentCommand, GetEverySalesTransactionQuery>
    {

    }
}

// Add GET endpoint to get every purchases documents for specific client
