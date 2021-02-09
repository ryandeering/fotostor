using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public interface IFollowingRepository
    {
        Following AddFollowing(Following follow);
        void RemoveFollowing(string FollowerID, string FollowingID);
        Following VerifyFollowing(Following follow);
    }
}
