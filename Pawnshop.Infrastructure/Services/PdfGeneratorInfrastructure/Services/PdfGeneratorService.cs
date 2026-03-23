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
            try
            {
                var templatePath = Path.Combine("C:\\Users\\Jc0b\\Desktop\\Pawnshop\\Pawnshop.Infrastructure\\Services\\PdfGeneratorInfrastructure\\Services\\PDFViews", $"{templateName}.hbs");
                //C:\Users\Jcob\Desktop\Pawnshop\Pawnshop.Infrastructure\PurchaseAgreementTemplate.hbs
                var templateContext = await File.ReadAllTextAsync(templatePath, cancellationToken);
                Handlebars.RegisterHelper("add", (writer, context, parameters) =>
                {
                    if (parameters.Length == 2 &&
                        double.TryParse(parameters[0]?.ToString(), out var a) &&
                        double.TryParse(parameters[1]?.ToString(), out var b))
                    {
                        writer.Write(a + b);
                    }
                    else
                    {
                        writer.Write("NaN");
                    }
                });


                var template = Handlebars.Compile(templateContext);

                var html = template(data);

                var pdf = await ConvertHtmlToPdfAsync(html, cancellationToken);

                return pdf;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private async Task<byte[]> ConvertHtmlToPdfAsync(string html, CancellationToken cancellationToken = default)
        {
            try
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
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}