namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;

    public interface ILikeRepository
    {
        Like AddLike(Like like);

        void RemoveLike(string UserID, string PostID);

        Like VerifyLike(string UserID, string PostID);
    }
}
