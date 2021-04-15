using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface IHashTagRepository
    {
        IEnumerable<Post> GetLatestPostsByHashTag(string hashTag);
        HashTag GetHashTag(string hashTag);
    }
}