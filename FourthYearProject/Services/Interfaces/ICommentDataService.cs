using System.Collections.Generic;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public interface ICommentDataService
    {
        Task<Comment> GetCommentById(int Comment_Id);
        Task<Comment> AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(int Comment_Id);
        Task<IEnumerable<Comment>> GetCommentsByPostId(int id);
    }
}