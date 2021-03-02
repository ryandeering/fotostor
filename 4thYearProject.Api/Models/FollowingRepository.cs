namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using System.Linq;

    //  [Route("api/[controller]")]
    //[ApiController]
    public class FollowingRepository : IFollowingRepository
    {
        private readonly AppDbContext _appDbContext;

        public FollowingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Following VerifyFollowing(Following follow)
        {


            bool following = _appDbContext.Followers.Where(f => f.Follower_ID == follow.Follower_ID && f.Followed_ID == follow.Followed_ID).Any();


            if (!following)
            {
                follow = null;
            }

            return follow;
        }

        public Following AddFollowing(Following follow)
        {


            if (_appDbContext.Followers.FirstOrDefault(f => f.Follower_ID == follow.Follower_ID && f.Followed_ID == follow.Followed_ID) != null)
            {
                return null;
            }
            var addedEntity = _appDbContext.Followers.Add(follow);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public void RemoveFollowing(string FollowerID, string FollowingID)
        {
            var foundFollower = _appDbContext.Followers.FirstOrDefault(f => f.Follower_ID == FollowerID);
            if (foundFollower == null) return;

            _appDbContext.Followers.Remove(foundFollower);
            _appDbContext.SaveChanges();
        }
    }
}
