using System.Collections.Generic;
using System.Linq;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public class HashTagRepository
{

    private readonly AppDbContext _appDbContext;

    public HashTagRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IEnumerable<Post> GetLatestPostsByHashTag(string hashTag)
    {
        return _appDbContext.Posts.Where(p => p.HashTags.Any(h => h.Content == hashTag))
            .OrderByDescending(p => p.UploadDate).Take(25);
    }






    }
}
