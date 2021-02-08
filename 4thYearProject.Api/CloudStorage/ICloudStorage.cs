using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace _4thYearProject.Api.CloudStorage
{
    public interface ICloudStorage
{
        Task<string> UploadFileAsync(byte[] imageFile, string fileNameForStorage);
        Task DeleteFileAsync(string fileNameForStorage);
    }
}
