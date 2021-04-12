using System.Linq;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class CommentsRepositoryUnitTests
    {

        [Fact]
        public void GetAllCommentsByPostId()
        {
            var i = 1;
            GenFu.GenFu.Configure<Comment>()
                .Fill(c => c.Id, () => i++);
            GenFu.GenFu.Configure<Comment>().Fill(c => c.PostId, 1);

            var test1 = GenFu.GenFu.ListOf<Comment>();


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Get Comments By Post Id Test")
                .Options;


            using var context = new AppDbContext(options);
            foreach (var comment in test1) context.Comments.Add(comment);
            context.SaveChanges();
            var repo = new CommentRepository(context);
            var comments = repo.GetCommentsByPostId(1);


            for (var j = 0; j < test1.Count; j++)
                Assert.Equal(test1.OrderByDescending(p => p.SubmittedOn).ElementAt(j).Body,
                    comments.OrderByDescending(p => p.SubmittedOn).ElementAt(j).Body);
        }


        [Fact]
        public void GetCommentById()
        {
            var i = 1;
            GenFu.GenFu.Configure<Comment>()
                .Fill(c => c.Id, () => i++);

            var test1 = GenFu.GenFu.ListOf<Comment>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Get Comments By Id Test")
                .Options;


            using var context = new AppDbContext(options);
            foreach (var comment in test1) context.Comments.Add(comment);
            context.SaveChanges();
            var repo = new CommentRepository(context);


            for (var j = 1; j < test1.Count; j++)
            {
                var comment = repo.GetCommentById(j);
                Assert.Equal(comment.Body, test1.First(c => c.Id == j).Body);
            }

        }


        [Fact]
        public void AddComment()
        {
            var i = 1;
            GenFu.GenFu.Configure<Comment>()
                .Fill(c => c.Id, () => i++);

            var test1 = GenFu.GenFu.New<Comment>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Add Comment Test")
                .Options;


            using var context = new AppDbContext(options);
            var repo = new CommentRepository(context);
            repo.AddComment(test1);
            var comment = repo.GetCommentById(1);
            Assert.Equal(test1.Body, comment.Body);
        }

        [Fact]
        public void UpdateComment()
        {
            var i = 1;
            GenFu.GenFu.Configure<Comment>()
                .Fill(c => c.Id, () => i++);
            var test1 = GenFu.GenFu.New<Comment>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Update Comment Test")
                .Options;

            using var context = new AppDbContext(options);
            context.Comments.Add(test1);
            context.SaveChanges();
            var repo = new CommentRepository(context);
            var comment = repo.GetCommentById(1);

            comment.Body = "This comment has been updated.";

            var updatedComment = repo.UpdateComment(comment);

            var updatedComment2 = repo.GetCommentById(1);

            Assert.Equal(updatedComment, updatedComment2);


        }


        [Fact]
        public void DeleteComment()
        {
            var i = 1;
            GenFu.GenFu.Configure<Comment>()
                .Fill(c => c.Id, () => i++);
            var test1 = GenFu.GenFu.ListOf<Comment>(3);
            var specificComment = test1.FirstOrDefault(c => c.Id == 1);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Update Comment Test")
                .Options;

            using var context = new AppDbContext(options);
            foreach (var comment in test1)
            {
                context.Comments.Add(comment);
            }
            var repo = new CommentRepository(context);
            var specificComment2 = repo.GetCommentById(1);
            test1.Remove(specificComment);
            repo.DeleteComment(1);

            Assert.DoesNotContain(specificComment2, context.Comments.ToList());
        }

        [Fact]
        public void DeleteComment_Fail()
        {

            var test1 = GenFu.GenFu.ListOf<Comment>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Delete Comment Fail Test")
                .Options;

            using var context = new AppDbContext(options);
            foreach (var comment in test1)
            {
                context.Comments.Add(comment);
            }

            context.SaveChanges();
            var repo = new CommentRepository(context);
            repo.DeleteComment(5);
            var commentsContextContent = context.Comments;
            Assert.Equal(test1.Count, commentsContextContent.ToList().Count);
        }

    }
}