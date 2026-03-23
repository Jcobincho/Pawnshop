namespace Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces
{
    public interface IFileStorageQueryService
    {
        string GetFileUrl(string fileKey);
        Task<(MemoryStream Stream, string ContentType)> GetFileWithMetadataAsync(string fileKey, CancellationToken cancellationToken);
    }
}
