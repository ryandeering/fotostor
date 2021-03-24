﻿namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using System.Collections.Generic;

    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsByPostId(int id);

        Comment GetCommentById(int Comment_ID);

        Comment AddComment(Comment comment);

        Comment UpdateComment(Comment comment);

        void DeleteComment(int Comment_ID);
    }
}