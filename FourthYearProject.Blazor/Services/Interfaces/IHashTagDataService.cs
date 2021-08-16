using System.Collections.Generic;
using System.Threading.Tasks;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services
{
    public interface IHashTagDataService
    {
        Task<IEnumerable<Post>> GetLatestPostsByHashTag(string hashTag);
        Task<HashTag> GetHashTag(HashTag hashTag);
    }
}