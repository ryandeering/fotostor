using System.Threading.Tasks;

namespace FourthYearProject.Api.CloudStorage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(byte[] imageFile, string fileNameForStorage);

        Task DeleteFileAsync(string fileNameForStorage);
    }
}