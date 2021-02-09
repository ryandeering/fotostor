using _4thYearProject.Shared.Models;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IFollowingDataService
    {
        Task<Following> AddFollowing(Following follow);
        Task RemoveFollowing(string Follower_ID, string Following_ID);
        // Task<IEnumerable<Following>> verifyFollowing(Following follow);
    }
}

