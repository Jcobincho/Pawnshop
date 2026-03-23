namespace Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces
{
    public interface IFileStorageEditService
    {
        Task<string> UploadFileAsync(byte[] file, string fileName, string contentType, CancellationToken cancellationToken);
        Task DeleteFileAsync(string s3Key, CancellationToken cancellationToken);
    }
}
