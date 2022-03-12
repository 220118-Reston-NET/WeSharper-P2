using System.IO;
using System.Threading.Tasks;

namespace WeSharper.APIPortal.BlobService.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    }
}