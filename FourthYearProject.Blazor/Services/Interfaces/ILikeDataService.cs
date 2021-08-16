using System.Threading.Tasks;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services
{
    public interface ILikeDataService
    {
        Task<Like> AddLike(Like like);
        Task RemoveLike(string Post_ID, string User_ID);
        Task<bool> VerifyLike(string Post_ID, string User_ID);
    }
}