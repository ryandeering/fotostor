using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IPostDataService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostDetails(int postId);
        Task<Post> AddPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(int postId);
        Task<IEnumerable<Post>> GetPostsByUserId(string id);
    }
}

