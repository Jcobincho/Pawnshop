using MassTransit;
using Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces;
using Pawnshop.Application.NotificationApplication.Interfaces;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Consumers.GenerateTransactionReport;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Consumers;

public sealed class GenerateTransactionReportConsumer : IConsumer<GenerateTransactionReportEvent>
{
    private readonly IPurchasesSaleTransactionQueryService _transactionQueryService;
    private readonly IReportGeneratorService _reportGeneratorService;
    private readonly IFileStorageEditService _fileStorageEditService;
    private readonly IFileStorageQueryService _fileStorageQueryService;
    private readonly INotificationService _notificationService;

    public GenerateTransactionReportConsumer(
        IPurchasesSaleTransactionQueryService transactionQueryService,
        IReportGeneratorService reportGeneratorService,
        IFileStorageEditService fileStorageEditService,
        IFileStorageQueryService fileStorageQueryService,
        INotificationService notificationService)
    {
        _transactionQueryService = transactionQueryService;
        _reportGeneratorService = reportGeneratorService;
        _fileStorageEditService = fileStorageEditService;
        _fileStorageQueryService = fileStorageQueryService;
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<GenerateTransactionReportEvent> context)
    {
        var message = context.Message;
        try
        {
            // 1. Fetch data
            var transactionData = await _transactionQueryService.GetPurchaseSaleTransactionInfoToAgreementAsync(message.TransactionId, context.CancellationToken);

            // 2. Generate PDF using QuestPDF
            var pdfBytes = await _reportGeneratorService.GenerateTransactionReportAsync(transactionData, context.CancellationToken);

            // 3. Upload to Minio
            var fileName = $"Report_{transactionData.Symbol.Replace("/", "_")}.pdf";
            var fileKey = await _fileStorageEditService.UploadFileAsync(pdfBytes, fileName, "application/pdf", context.CancellationToken);

            // 4. Get secure URL
            var reportUrl = _fileStorageQueryService.GetFileUrl(fileKey);

            // 5. Notify user via SignalR
            await _notificationService.NotifyReportReadyAsync(message.UserId, reportUrl, fileName, context.CancellationToken);
        }
        catch (Exception ex)
        {
            // Notify user about failure
            await _notificationService.NotifyErrorAsync(message.UserId, $"Failed to generate report: {ex.Message}", context.CancellationToken);
            throw; // Re-throw for MassTransit retry policy
        }
    }
}
