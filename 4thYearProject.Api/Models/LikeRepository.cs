namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using System.Linq;

    //  [Route("api/[controller]")]
    //[ApiController]
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _appDbContext;

        public LikeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public Like VerifyLike(Like like)
        {

            if (!_appDbContext.Likes.Any(l => l.User_ID == like.User_ID && l.Post_ID == like.Post_ID))
            {
                return null;
            }

            return like;

        }

        public Like AddLike(Like like)
        {
            if (_appDbContext.Likes.FirstOrDefault(l => l.User_ID == like.User_ID && l.Post_ID == like.Post_ID) != null)
            {
                return null;
            }
            var addedEntity = _appDbContext.Likes.Add(like);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }


        public void RemoveLike(string UserID, string PostID)
        {
            var foundLike = _appDbContext.Likes.FirstOrDefault(l => l.User_ID == UserID);
            if (foundLike == null) return;

            _appDbContext.Likes.Remove(foundLike);
            _appDbContext.SaveChanges();
        }

        public Like VerifyLike(string PostId, string UserId)
        {
            var foundLike = _appDbContext.Likes.FirstOrDefault(p => p.User_ID == UserId && p.Post_ID == PostId);
            if (foundLike == null) return null;

            return foundLike;
        }

    }
}
