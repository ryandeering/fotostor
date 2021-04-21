using System.Collections.Generic;
using System.Linq;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    //  [Route("api/[controller]")]
    //[ApiController]
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Comment> GetCommentsByPostId(int id)
        {
            return _appDbContext.Comments.Where(c => c.PostId.Equals(id))
                .OrderBy(c => c.SubmittedOn);
        }

        public Comment GetCommentById(int Comment_ID)
        {
            return _appDbContext.Comments.FirstOrDefault(c => c.Id == Comment_ID);
        }

        public Comment AddComment(Comment comment)
        {
            var addedEntity = _appDbContext.Comments.Add(comment);

            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public Comment UpdateComment(Comment comment)
        {
            var foundComment = _appDbContext.Comments.FirstOrDefault(c => c.Id == comment.Id);

            if (foundComment != null)
            {
                _appDbContext.SaveChanges();

                return foundComment;
            }

            return null;
        }

        public void DeleteComment(int Comment_ID)
        {
            var foundComment = _appDbContext.Comments.FirstOrDefault(c => c.Id == Comment_ID);
            if (foundComment == null) return;

            _appDbContext.Comments.Remove(foundComment);
            _appDbContext.SaveChanges();
        }
    }
}