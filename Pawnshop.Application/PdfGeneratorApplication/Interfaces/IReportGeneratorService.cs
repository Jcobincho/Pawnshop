namespace Pawnshop.Application.PdfGeneratorApplication.Interfaces;

public interface IReportGeneratorService
{
    Task<byte[]> GenerateTransactionReportAsync<T>(T data, CancellationToken cancellationToken);
}
