using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace _4thYearProject.Api.Models
{
    //  [Route("api/[controller]")]
    //[ApiController]
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;


        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public IEnumerable<Post> GetAllPostsbyFollowing(string id)
        {
            
   
                var followings = _appDbContext.Followers.AsNoTracking().ToList();

                var followingids = new HashSet<string>();

                followingids.Add(id);

                foreach (var follow in followings)
                    if (follow.Follower_ID.Equals(id))
                        followingids.Add(follow.Followed_ID);

                var posts = _appDbContext.Posts.Include("Comments").Where(x => followingids.Any(n => n == x.UserId))
                    .OrderByDescending(p => p.UploadDate).Distinct().AsNoTracking();


                return posts.Where(p => !p.PostDeleted);
        }

        public Post GetPostById(int postId)
        {
            return _appDbContext.Posts.AsNoTracking().FirstOrDefault(p => p.PostId == postId);
        }

        public IEnumerable<Post> GetPostsByUserId(string id)
        {
            return _appDbContext.Posts.AsNoTracking().Where(p => p.UserId.Equals(id) && !p.PostDeleted)
                .OrderByDescending(p => p.UploadDate);
        }

        public Post AddPost(Post post)
        {
            var addedEntity = _appDbContext.Posts.Add(post);

            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Post UpdatePost(Post post)
        {
            var foundPost = _appDbContext.Posts.FirstOrDefault(p => p.PostId == post.PostId);

            if (foundPost != null)
            {
                foundPost.Caption = post.Caption;
                foundPost.HashTags = post.HashTags;
                foundPost.PostDeleted = post.PostDeleted;
                _appDbContext.SaveChanges();

                return foundPost;
            }

            return null;
        }

        public void DeletePost(int postId)
        {
            var foundPost = _appDbContext.Posts.FirstOrDefault(p => p.PostId == postId);
            if (foundPost == null) return;

            foundPost.PostDeleted = true;
            UpdatePost(foundPost);
            _appDbContext.SaveChanges();
        }
    }
}