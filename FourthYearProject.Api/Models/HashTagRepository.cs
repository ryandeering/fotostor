using System.Collections.Generic;
using System.Linq;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models
{
    public class HashTagRepository : IHashTagRepository
    {
        private readonly AppDbContext _appDbContext;

        public HashTagRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Post> GetLatestPostsByHashTag(string hashTag)
        {
            return _appDbContext.Posts.Where(p => p.HashTags.Any(h => h.Content.EndsWith(hashTag)))
                .OrderByDescending(p => p.UploadDate).Take(25).ToList();
        }

        public HashTag GetHashTag(string hashTag)
        {
            var hashTagFound = _appDbContext.Hashtags.FirstOrDefault(h => h.Content.Equals(hashTag));

            if (hashTagFound == null)
            {
                var newhTag = new HashTag
                {
                    Content = hashTag
                };

                _appDbContext.Hashtags.Add(newhTag);
                _appDbContext.SaveChanges();
                return newhTag;
            }

            return hashTagFound;
        }
    }
}