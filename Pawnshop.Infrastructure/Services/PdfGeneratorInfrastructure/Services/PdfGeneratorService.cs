using HandlebarsDotNet;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace Pawnshop.Infrastructure.Services.PdfGeneratorInfrastructure.Services
{
    internal sealed class PdfGeneratorService : IPdfGeneratorService
    {
        public async Task<byte[]> GeneratePdfAsync<T>(T data, string templateName, CancellationToken cancellationToken)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "PDFViews", $"{templateName}.hbs");
            var templateContext = await File.ReadAllTextAsync(templatePath);
            var template = Handlebars.Compile(templateContext);

            var html = template(data);

            var pdf = await ConvertHtmlToPdfAsync(html);

            return pdf;
        }

        private async Task<byte[]> ConvertHtmlToPdfAsync(string html, CancellationToken cancellationToken = default)
        {
            await new BrowserFetcher().DownloadAsync();

            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
            });

            using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);
            await page.WaitForSelectorAsync("body");

            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "20mm",
                    Right = "20mm",
                    Bottom = "20mm",
                    Left = "20mm"
                }
            });

            return pdfBytes;
        }
    }
}
