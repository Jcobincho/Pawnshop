using MassTransit;
using Microsoft.AspNetCore.Http;
using Pawnshop.Application.FileStorageApplication.Interfaces;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Domain.Entities.FileStorage;
using Pawnshop.Domain.Entities.Transactions;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Consumers
{
    internal sealed class GenerateAgreementCustomer : IConsumer<GenerateAgreementEvent>
    {
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IFileStorageEditService _fileStorageEditService;
        private readonly IFileStorageQueryService _fileStorageQueryService;

        public GenerateAgreementCustomer(IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService, IPdfGeneratorService pdfGeneratorService, IFileStorageEditService fileStorageEditService, IFileStorageQueryService fileStorageQueryService)
        {
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
            _pdfGeneratorService = pdfGeneratorService;
            _fileStorageEditService = fileStorageEditService;
            _fileStorageQueryService = fileStorageQueryService;
        }

        public async Task Consume(ConsumeContext<GenerateAgreementEvent> context)
        {
            try
            {
                var message = context.Message;

                var transactionData = await _purchasesSaleTransactionQueryService.GetPurchaseSaleTransactionInfoToAgreementAsync(message.PurchasesSaleTransactionId, context.CancellationToken);

                if(transactionData != null)
                {
                    var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(transactionData, "PurchaseAgreementTemplate", context.CancellationToken);

                    var fileName = $"{transactionData.Symbol}.pdf";

                    // upload file in mimio s3
                    var uploadKey = await _fileStorageEditService.UploadFileAsync(pdfBytes, fileName, "application/pdf", context.CancellationToken);
                    var url = _fileStorageQueryService.GetFileUrl(uploadKey);


                    // TO DO: Przerowić na zapis z dedykowanej metody

                    //var agreement = new PurchaseSaleTransactionAgreement
                    //{
                    //    PurchaseSaleTransactionId = message.PurchasesSaleTransactionId,
                    //    Symbol = fileName,
                    //    Url = url,
                    //    ContentType = "application/pdf",
                    //    TotalBytes = pdfBytes.LongLength,
                    //    S3Key = uploadKey
                    //};
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
