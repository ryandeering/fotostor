﻿using System;
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


            if (userPosts.Count().Equals(0))
            {
                var hashtags = _appDbContext.Hashtags.GroupBy(h => h.Content)
                    .Select(group => new {Content = group.Key, Count = group.Count()}).OrderByDescending(h => h.Count);

                var suggestedPostsNoInterests = new List<Post>();

                foreach (var HashTag in hashtags)
                {
                    var HashTagActual = _appDbContext.Hashtags.First(ht => ht.Content.Contains(HashTag.Content));
                    var PostWithHashTag = _appDbContext.Posts.Where(p => p.HashTags.Contains(HashTagActual))
                        .OrderByDescending(p => p.Likes).FirstOrDefault();
                    suggestedPostsNoInterests.Add(PostWithHashTag);
                }

                try
                {
                    foreach (var post in suggestedPostsNoInterests) post.HashTags.Clear();
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Possible hashtag with no posts found?");
                }


                return suggestedPostsNoInterests.OrderBy(p => Guid.NewGuid()).Distinct().ToList();
            }


            var userHashTags = new List<HashTag>();
            foreach (var userPost in userPosts)
            foreach (var hashTag in userPost.HashTags)
                userHashTags.Add(hashTag);

            var query = userHashTags.GroupBy(h => h.Content)
                .Select(group => new {Content = group.Key, Count = group.Count()}).OrderByDescending(h => h.Count);

            var items = query.Take(4);

            var suggestedPosts = new List<Post>();

            try
            {
                suggestedPosts = (from item in items
                    select _appDbContext.Hashtags.FirstOrDefault(ht => ht.Content.Equals(item.Content))
                    into foundHashTag
                    select _appDbContext.Posts.Where(p => p.HashTags.Contains(foundHashTag) && p.UserId != id)
                    into foundPosts
                    select foundPosts.OrderByDescending(x => x.Likes).First()).ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            var suggestedPostsFinal = suggestedPosts.OrderBy(p => Guid.NewGuid()).Distinct().ToList(); //shuffle

            foreach (var post in suggestedPostsFinal) post.HashTags.Clear();

            return suggestedPostsFinal;
        }
    }
}