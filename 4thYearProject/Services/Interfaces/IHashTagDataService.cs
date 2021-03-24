using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public interface IHashTagDataService
{
    Task<IEnumerable<Post>> GetLatestPostsByHashTag(string hashTag);
    Task<HashTag> GetHashTag(HashTag hashTag);



}
}
