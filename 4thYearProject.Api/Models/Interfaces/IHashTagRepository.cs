using System.Collections.Generic;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public interface IHashTagRepository
    {
        IEnumerable<Post> GetLatestPostsByHashTag(string hashTag);
        HashTag GetHashTag(string hashTag);
    }
}