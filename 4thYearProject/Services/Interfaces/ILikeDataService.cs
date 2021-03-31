using _4thYearProject.Shared.Models;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface ILikeDataService
    {
        Task<Like> AddLike(Like like);
        Task RemoveLike(string Post_ID, string User_ID);
        Task<bool> VerifyLike(string Post_ID, string User_ID);
    }
}

