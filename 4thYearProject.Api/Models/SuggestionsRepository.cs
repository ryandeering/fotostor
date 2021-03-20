using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace _4thYearProject.Api.Models
{
    public class SuggestionsRepository : ISuggestionsRepository
    {
        private readonly AppDbContext _appDbContext;

        public SuggestionsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public IEnumerable<Post> GetSuggestions(string id)
        {
            var userPosts = _appDbContext.Posts
                .Where(x => x.UserId == id)
                .Include(x => x.HashTags);

            var userHashTags = new List<HashTag>();
            foreach (var userPost in userPosts)
            foreach (var hashTag in userPost.HashTags)
                userHashTags.Add(hashTag);

            var query = userHashTags.GroupBy(h => h.Content)
                .Select(group => new {Content = group.Key, Count = group.Count()}).OrderByDescending(h => h.Count);


            var items = query.Take(4);

            var suggestedPosts = new List<Post>();

            foreach (var item in items)
            {
                var foundHashTag = _appDbContext.Hashtags.Where(ht => ht.Content.Equals((item.Content)));
                var foundPost = _appDbContext.Posts.OrderByDescending(p => p.Likes).FirstOrDefault();
                suggestedPosts.Add(foundPost);
            }

            var suggestedPostsFinal = suggestedPosts.OrderBy(p => Guid.NewGuid()).ToList();


            return suggestedPostsFinal;
        }
    }
}