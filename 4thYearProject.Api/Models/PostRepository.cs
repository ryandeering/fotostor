using System;
using System.Collections.Generic;
using System.Linq;
using _4thYearProject.Shared.Models;


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

        public IEnumerable<Post> GetAllPosts()
        {
            return _appDbContext.Posts;
        }

        public Post GetPostById(int postId)
        {
            return _appDbContext.Posts.FirstOrDefault(p => p.PostId == postId);
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
              //  foundEmployee.CountryId = employee.CountryId;
             //   foundEmployee.MaritalStatus = employee.MaritalStatus;
               // foundEmployee.BirthDate = employee.BirthDate;
   
                _appDbContext.SaveChanges();

                return foundPost;
            }

            return null;
        }

        public void DeletePost(int postId)
        {
            var foundPost = _appDbContext.Posts.FirstOrDefault(p => p.PostId == postId);
            if (foundPost == null) return;

            _appDbContext.Posts.Remove(foundPost);
            _appDbContext.SaveChanges();
        }
    }



}

