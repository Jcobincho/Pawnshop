using Amazon.S3;
using Amazon.S3.Model;
using Pawnshop.Application.FileStorageApplication.FileStorage.Interfaces;
using Pawnshop.Domain.Exceptions;
using Pawnshop.Domain.FileStorage;

namespace Pawnshop.Infrastructure.Services.FileStorageInfrastructure.FileStorage.Services
{
    internal sealed class FileStorageEditService : IFileStorageEditService
    {
        private readonly S3Configuration _s3Config;
        private readonly AmazonS3Client _s3Client;

        public FileStorageEditService(S3Configuration s3Config)
        {
            _s3Config = s3Config;
            var connectionConfig = new AmazonS3Config
            {
                ServiceURL = _s3Config.S3Url,
                ForcePathStyle = true,
                UseHttp = true
            };
            _s3Client = new AmazonS3Client(_s3Config.AccessKey, _s3Config.SecretKey, connectionConfig);
        }

        public async Task<string> UploadFileAsync(byte[] file, string fileName, string contentType, CancellationToken cancellationToken)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            try
            {
                await using var stream = new MemoryStream(file);
                await _s3Client.PutObjectAsync(new PutObjectRequest()
                {
                    BucketName = _s3Config.BucketName,
                    Key = uniqueFileName,
                    InputStream = stream,
                    ContentType = contentType
                }, cancellationToken);
            }
            catch (AmazonS3Exception e)
            {
                if (e.ErrorCode == "NoSuchBucket")
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = _s3Config.BucketName,
                    };
                    await _s3Client.PutBucketAsync(putBucketRequest, cancellationToken);
                    await using var stream = new MemoryStream(file);
                    await _s3Client.PutObjectAsync(new PutObjectRequest()
                    {
                        BucketName = _s3Config.BucketName,
                        Key = uniqueFileName,
                        InputStream = stream,
                        ContentType = contentType
                    }, cancellationToken);
                    return uniqueFileName;
                }
                throw new BadRequestException(e.ErrorCode);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
            return uniqueFileName;
        }

        public async Task DeleteFileAsync(string s3Key, CancellationToken cancellationToken)
        {
            try
            {
                await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
                {
                    BucketName = _s3Config.BucketName,
                    Key = s3Key
                }, cancellationToken);
            }
            catch (AmazonS3Exception e)
            {
                throw new BadRequestException(e.ErrorCode);
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
        }
    }
}