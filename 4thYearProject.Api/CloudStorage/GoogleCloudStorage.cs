namespace _4thYearProject.Api.CloudStorage
{
    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Storage.V1;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Threading.Tasks;

    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly string bucketName;

        private readonly GoogleCredential googleCredential;

        private readonly StorageClient storageClient;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        public async Task<string> UploadFileAsync(byte[] imageFile, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream(imageFile))
            {
                var dataObject =
                    await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                return dataObject.MediaLink;
            }
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }
    }
}
