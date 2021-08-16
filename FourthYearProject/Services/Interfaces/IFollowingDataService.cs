using System.Collections.Generic;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public interface IFollowingDataService
    {
        Task<Following> AddFollowing(Following follow);
        Task RemoveFollowing(string Follower_ID, string Following_ID);
        Task<Following> verifyFollowing(string Follower_ID, string Following_ID);
        Task<List<Following>> GetFollowers(string Following_ID);
        Task<List<Following>> GetFollowing(string Follower_ID);
        Task<List<FeedProfileData>> GetFollowingUserData(string Following_ID);
    }
}