using FourthYearProject.Shared.Models;

namespace FourthYearProject.Api.Models
{
    public interface ILikeRepository
    {
        Like AddLike(Like like);

        void RemoveLike(string UserID, string PostID);

        Like VerifyLike(string PostId, string UserId);

        int GetLikeCount(string Post_ID);
    }
}