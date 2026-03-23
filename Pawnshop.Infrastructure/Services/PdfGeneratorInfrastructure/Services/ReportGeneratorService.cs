using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;

namespace Pawnshop.Infrastructure.Services.PdfGeneratorInfrastructure.Services;

public sealed class ReportGeneratorService : IReportGeneratorService
{
    public ReportGeneratorService()
    {
        // QuestPDF requires setting the license type
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GenerateTransactionReportAsync<T>(T data, CancellationToken cancellationToken)
    {
        // For demonstration, we create a simple report. 
        // In a real scenario, you'd use the data to populate the document.
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Text("Transaction Report").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content().PaddingVertical(1, Unit.Centimetre).Column(x =>
                {
                    x.Spacing(10);
                    x.Item().Text($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    
                    // Here you would use 'data' to display transaction details
                    if (data != null)
                    {
                        x.Item().Text($"Report Data Type: {typeof(T).Name}");
                    }
                    
                    x.Item().PaddingTop(20).LineHorizontal(1);
                    x.Item().Text("This is an automatically generated report from Pawnshop System.").Italic();
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
            });
        });

        using var stream = new MemoryStream();
        document.GeneratePdf(stream);
        return await Task.FromResult(stream.ToArray());
    }
}
