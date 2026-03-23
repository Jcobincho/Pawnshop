using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pawnshop.Domain.FileStorage;

namespace Pawnshop.Infrastructure.Persistance.Extensions
{
    public static class FileStorageConfiguration
    {
        public static IServiceCollection AddFileStorageConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var s3Config = new S3Configuration();
            configuration.GetSection("S3Service").Bind(s3Config);
            services.AddSingleton(s3Config);

            return services;
        }
    }
}
