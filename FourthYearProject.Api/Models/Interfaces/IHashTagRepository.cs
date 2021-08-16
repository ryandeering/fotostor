using System.Collections.Generic;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models
{
    public interface IHashTagRepository
    {
        IEnumerable<Post> GetLatestPostsByHashTag(string hashTag);
        HashTag GetHashTag(string hashTag);
    }
}