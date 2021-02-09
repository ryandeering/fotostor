﻿using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public interface ILikeRepository
    {
        Like AddLike(Like like);
        void RemoveLike(string UserID, string PostID);
        Like VerifyLike(Like like);
    }
}
