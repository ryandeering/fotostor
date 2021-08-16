using System.Collections.Generic;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models
{
    public interface IFollowingRepository
    {
        Following AddFollowing(Following follow);

        void RemoveFollowing(string FollowerID, string FollowingID);

        Following VerifyFollowing(string FollowerID, string FollowedID);

        List<Following> GetFollowers(string FollowingID);

        List<Following> GetFollowing(string FollowingID);
    }
}