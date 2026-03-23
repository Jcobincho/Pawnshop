namespace Pawnshop.Application.PdfGeneratorApplication.Interfaces
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GeneratePdfAsync<T>(T data, string templateName, CancellationToken cancellationToken);
    }
}
