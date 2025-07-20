using Amazon.S3.Model;
using Amazon.S3;
using Amazon.Util;
using Pawnshop.Application.FileStorageApplication.Interfaces;
using Pawnshop.Domain.FileStorage;
using HandlebarsDotNet;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.FileStorageInfrastructure.Services
{
    internal sealed class FileStorageQueryService : IFileStorageQueryService
    {
        private readonly S3Configuration _s3Config;
        private readonly AmazonS3Client _s3Client;

        public FileStorageQueryService(S3Configuration s3Config)
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

        public string GetFileUrl(string fileKey)
        {
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _s3Config.BucketName,
                    Key = fileKey,
                    Expires = System.DateTime.MaxValue
                };

                var url = _s3Client.GetPreSignedURL(request);
                url = url.Replace("https", "http");
                url = url.Replace("minio:9000", "localhost:9000");
                return url;
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

        public async Task<(MemoryStream Stream, string ContentType)> GetFileWithMetadataAsync(string fileKey, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = _s3Config.BucketName,
                    Key = fileKey
                };

                var response = await _s3Client.GetObjectAsync(request, cancellationToken);

                await using var responseStream = response.ResponseStream;
                var memoryStream = new MemoryStream();
                await responseStream.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Position = 0;

                return (memoryStream, response.Headers.ContentType);
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
