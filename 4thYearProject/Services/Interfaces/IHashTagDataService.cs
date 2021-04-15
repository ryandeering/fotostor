using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IHashTagDataService
    {
        Task<IEnumerable<Post>> GetLatestPostsByHashTag(string hashTag);
        Task<HashTag> GetHashTag(HashTag hashTag);



    }
}
