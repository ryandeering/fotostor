using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
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
            var userPosts = _appDbContext.Posts.Include("HashTags").Where(x => x.UserId == id)
                .Include(x => x.HashTags);


            if (userPosts.Count(up => !up.PostDeleted).Equals(0))
            {
                var hashtags = _appDbContext.Hashtags.GroupBy(h => h.Content)
                    .Select(group => new {Content = group.Key, Count = group.Count()}).OrderByDescending(h => h.Count)
                    .Take(3);

                var suggestedPostsNoInterests = new List<Post>();

                foreach (var HashTag in hashtags)
                {
                    var HashTagActual = _appDbContext.Hashtags.First(ht => ht.Content.Contains(HashTag.Content));
                    var PostWithHashTag = _appDbContext.Posts.Include("HashTags")
                        .Where(p => p.HashTags.Contains(HashTagActual))
                        .OrderBy(p => p.Likes).FirstOrDefault(); //gets the most popular post per popular hashtag
                    suggestedPostsNoInterests.Add(PostWithHashTag);
                }

                foreach (var post in suggestedPostsNoInterests) post.HashTags.Clear();


                return suggestedPostsNoInterests.OrderByDescending(p => p.UploadDate).Distinct().ToList();
            }


            var userHashTags = userPosts.SelectMany(userPost => userPost.HashTags).Distinct().ToList();

            List<HashTag> hashTags = new List<HashTag>();

            foreach (var hashTag in userHashTags)
            {
                hashTags.Add(_appDbContext.Hashtags.Include(h => h.Posts).ThenInclude(p => p.Comments).First(p => p.Id == hashTag.Id));
            }



            //foreach (var likedPost in userLikesPosts)
            //    userLikesPosts.Add(_appDbContext.Posts.Include("HashTags").First(up => up.PostId == likedPost.PostId));

            //var userHashTagsLikesPosts = userLikesPosts.SelectMany(userPost => userPost.HashTags).ToList();

            //var query = userHashTagsLikesPosts.GroupBy(h => h.Content)
            //    .Select(group => new {Content = group.Key, Count = group.Count()}).OrderByDescending(h => h.Count);


            var items = hashTags.Take(4);

            var suggestedPosts = new List<Post>();
            try
            {

                foreach (var hashtag in items)
                {
                    suggestedPosts.Add(hashtag.Posts.OrderBy(p => p.Comments.Count).First());
                }//based on engagement



                //suggestedPosts = (from item in items
                //        select _appDbContext.Hashtags.FirstOrDefault(ht => ht.Content.Equals(item.Content))
                //        into foundHashTag
                //        select _appDbContext.Posts.Include("Comments")
                //            .Where(p => p.HashTags.Contains(foundHashTag) && p.UserId != id)
                //        into foundPosts
                //        select foundPosts.OrderBy(x => x.Likes).First())
                //    .ToList(); //gets posts that have the same hashtag, but do NOT have the same author 
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            var suggestedPostsFinal = suggestedPosts.OrderBy(p => Guid.NewGuid()).Distinct().ToList(); //shuffle

            foreach (var post in suggestedPostsFinal) post.HashTags.Clear();

            return suggestedPostsFinal.Where(p => !p.PostDeleted);
        }
    }
}