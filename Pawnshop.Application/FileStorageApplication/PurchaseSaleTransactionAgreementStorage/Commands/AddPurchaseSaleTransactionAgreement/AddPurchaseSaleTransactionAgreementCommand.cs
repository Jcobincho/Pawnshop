using Pawnshop.Application.Common.Base;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Responses;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Commands.AddPurchaseSaleTransactionAgreement
{
    public sealed class AddPurchaseSaleTransactionAgreementCommand : BaseCommand<AddPurchaseSaleTransactionAgreementResponse>
    {
        [Required(ErrorMessage = "Document ID is required.")]
        public Guid PurchaseSaleTransactionId { get; set; }

        [Required(ErrorMessage = "Symbol is required.")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "Url is required.")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Content type is required.")]
        public string ContentType { get; set; }

        [Required(ErrorMessage = "Total bytes are required.")]
        public long TotalBytes { get; set; }

        [Required(ErrorMessage = "Key is required.")]
        public string S3Key { get; set; }
    }
}
