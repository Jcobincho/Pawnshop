using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.FileStorageApplication.Interfaces
{
    public interface IFileStorageQueryService
    {
        string GetFileUrl(string fileKey);
        Task<(MemoryStream Stream, string ContentType)> GetFileWithMetadataAsync(string fileKey, CancellationToken cancellationToken);
    }
}
