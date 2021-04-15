using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace _4thYearProject.Api.Models
{
    //  [Route("api/[controller]")]
    //[ApiController]
    public class FollowingRepository : IFollowingRepository
    {
        private readonly AppDbContext _appDbContext;

        public FollowingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Following VerifyFollowing(string FollowerID, string FollowedID)
        {
            var foundFollower =
                _appDbContext.Followers.FirstOrDefault(f => f.Follower_ID == FollowerID && f.Followed_ID == FollowedID);
            if (foundFollower == null) return null;

            return foundFollower;
        }

        public Following AddFollowing(Following follow)
        {
            if (_appDbContext.Followers.FirstOrDefault(f =>
                f.Follower_ID == follow.Follower_ID && f.Followed_ID == follow.Followed_ID) != null) return null;
            var addedEntity = _appDbContext.Followers.Add(follow);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public void RemoveFollowing(string FollowerID, string FollowingID)
        {
            var foundFollower =
                _appDbContext.Followers.FirstOrDefault(f =>
                    f.Follower_ID == FollowerID && f.Followed_ID == FollowingID);
            if (foundFollower == null) return;

            _appDbContext.Followers.Remove(foundFollower);
            _appDbContext.SaveChanges();
        }

        public List<Following> GetFollowers(string FollowingID)
        {
            var foundFollowers = _appDbContext.Followers.Where(f => f.Followed_ID == FollowingID).ToList();
            return foundFollowers;
        }

        public List<Following> GetFollowing(string FollowingID)
        {
            var foundFollowers = _appDbContext.Followers.Where(f => f.Follower_ID == FollowingID).ToList();
            return foundFollowers;
        }




    }
}