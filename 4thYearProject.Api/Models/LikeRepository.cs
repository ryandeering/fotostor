using System.Linq;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    //  [Route("api/[controller]")]
    //[ApiController]
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _appDbContext;

        public LikeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Like AddLike(Like like)
        {
            if (_appDbContext.Likes.FirstOrDefault(l => l.User_ID == like.User_ID && l.Post_ID == like.Post_ID) !=
                null) return new Like();
            var addedEntity = _appDbContext.Likes.Add(like);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public void RemoveLike(string UserID, string PostID)
        {
            var foundLike = _appDbContext.Likes.FirstOrDefault(l => l.User_ID == UserID && l.Post_ID == PostID);
            if (foundLike == null) return;

            _appDbContext.Likes.Remove(foundLike);
            _appDbContext.SaveChanges();
        }

        public Like VerifyLike(string PostId, string UserId)
        {
            var foundLikes = _appDbContext.Likes.Where(p => p.User_ID == UserId);
            var foundLike = foundLikes.FirstOrDefault(p => p.Post_ID == PostId);

            return foundLike;
        }

        public int GetLikeCount(string Post_ID)
        {
            var foundLikes = _appDbContext.Likes.Where(p => p.Post_ID == Post_ID);
            return foundLikes.Count();
        }
    }
}