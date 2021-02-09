using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetPostsByUserId(string id);
        IEnumerable<Post> GetAllPostsbyFollowing(string id);
        Post GetPostById(int postId);
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(int postId);
    }
}
