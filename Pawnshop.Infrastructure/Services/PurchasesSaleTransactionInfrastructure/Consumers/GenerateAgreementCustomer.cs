using MassTransit;
using MediatR;
using Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces;
using Pawnshop.Application.FileStorageApplication.PurchaseSaleTransactionAgreementStorage.Commands.AddPurchaseSaleTransactionAgreement;
using Pawnshop.Application.NotificationApplication.Interfaces;
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
        private readonly INotificationService _notificationService;

        public GenerateAgreementCustomer(IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService, IPdfGeneratorService pdfGeneratorService, IFileStorageEditService fileStorageEditService, IFileStorageQueryService fileStorageQueryService, IMediator mediator, INotificationService notificationService)
        {
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
            _pdfGeneratorService = pdfGeneratorService;
            _fileStorageEditService = fileStorageEditService;
            _fileStorageQueryService = fileStorageQueryService;
            _mediator = mediator;
            _notificationService = notificationService;
        }

        public async Task Consume(ConsumeContext<GenerateAgreementEvent> context)
        {
            var message = context.Message;

            try
            {
                var transactionData = await _purchasesSaleTransactionQueryService.GetPurchaseSaleTransactionInfoToAgreementAsync(message.PurchasesSaleTransactionId, context.CancellationToken);

                if (transactionData != null)
                {
                    var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(transactionData, "PurchaseAgreementTemplate", context.CancellationToken); 

                    var fileName = $"{transactionData.Symbol}.pdf";

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
                        await _notificationService.NotifyReportReadyAsync(message.UserId.ToString(), url, fileName, context.CancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                await _notificationService.NotifyErrorAsync(message.UserId.ToString(), $"Failed to generate report: {ex.Message}", context.CancellationToken);
                throw;
            }
        }
    }
}
