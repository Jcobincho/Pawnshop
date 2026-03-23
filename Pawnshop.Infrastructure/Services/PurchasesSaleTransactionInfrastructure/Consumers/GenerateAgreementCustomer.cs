using MassTransit;
using MediatR;
using Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Commands.AddPurchaseSaleTransactionAgreement;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;


namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Consumers
{
    internal sealed class GenerateAgreementCustomer : IConsumer<GenerateAgreementEvent>
    {
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IFileStorageEditService _fileStorageEditService;
        private readonly IFileStorageQueryService _fileStorageQueryService;
        private readonly IMediator _mediator;

        public GenerateAgreementCustomer(IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService, IPdfGeneratorService pdfGeneratorService, IFileStorageEditService fileStorageEditService, IFileStorageQueryService fileStorageQueryService, IMediator mediator)
        {
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
            _pdfGeneratorService = pdfGeneratorService;
            _fileStorageEditService = fileStorageEditService;
            _fileStorageQueryService = fileStorageQueryService;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GenerateAgreementEvent> context)
        {
            try
            {
                var message = context.Message;

                var transactionData = await _purchasesSaleTransactionQueryService.GetPurchaseSaleTransactionInfoToAgreementAsync(message.PurchasesSaleTransactionId, context.CancellationToken);

                if (transactionData != null)
                {
                    var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(transactionData, "PurchaseAgreementTemplate", context.CancellationToken); // 

                    var fileName = $"{transactionData.Symbol}.pdf";

                    // upload file in mimio s3
                    var uploadKey = await _fileStorageEditService.UploadFileAsync(pdfBytes, fileName, "application/pdf", context.CancellationToken);
                    var url = _fileStorageQueryService.GetFileUrl(uploadKey);


                    var command = new AddPurchaseSaleTransactionAgreementCommand
                    {
                        PurchaseSaleTransactionId = message.PurchasesSaleTransactionId,
                        Symbol = fileName,
                        Url = url,
                        ContentType = "application/pdf",
                        TotalBytes = pdfBytes.Length,
                        S3Key = uploadKey,
                    };

                    var resposne = await _mediator.Send(command, context.CancellationToken);

                    if (resposne != null)
                    {
                        // powiadom uzytkownika
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
