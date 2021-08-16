using System.Collections.Generic;
using System.Threading.Tasks;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services
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