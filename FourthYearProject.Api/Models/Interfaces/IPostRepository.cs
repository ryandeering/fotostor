using System.Collections.Generic;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPostsByUserId(string id);

        IEnumerable<Post> GetAllPostsbyFollowing(string id);

        Post GetPostById(int postId);

        Post AddPost(Post post);

        Post UpdatePost(Post post);

        void DeletePost(int postId);
    }
}