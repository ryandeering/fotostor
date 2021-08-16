using System.Collections.Generic;
using System.Threading.Tasks;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services
{
    public interface IPostDataService
    {
        Task<Post> GetPostDetails(int postId);
        Task<Post> AddPost(Post post);
        Task<IEnumerable<Post>> GetAllPostsbyFollowing(string id);
        Task UpdatePost(Post post);
        Task DeletePost(int postId);
        Task<IEnumerable<Post>> GetPostsByUserId(string id);
    }
}