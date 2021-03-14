namespace _4thYearProject.Api.CloudStorage
{
    using System.Threading.Tasks;

    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(byte[] imageFile, string fileNameForStorage);

        Task DeleteFileAsync(string fileNameForStorage);
    }
}
