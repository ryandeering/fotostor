using FourthYearProject.Api.Models;
using FourthYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace FourthYearProject.UnitTesting
{

    public class LikeRepositoryUnitTests
    {

        [Fact]
        public void VerifyLikeTest_Success()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Likes.Add(expectedLike);
            context.SaveChanges();
            var repo = new LikeRepository(context);
            var actualLike = repo.VerifyLike(expectedLike.Post_ID, expectedLike.User_ID);

            Assert.Equal(expectedLike.User_ID, actualLike.User_ID);
        }


        [Fact]
        public void VerifyLikeTest_Fail()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Likes.Add(expectedLike);
            context.SaveChanges();
            var repo = new LikeRepository(context);
            var actualLike = repo.VerifyLike(expectedLike.Post_ID, "not the right id");

            Assert.Null(actualLike);
        }

        [Fact]
        public void AddLike_Success()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new LikeRepository(context);
            repo.AddLike(expectedLike);

            var actualLike = repo.VerifyLike(expectedLike.Post_ID, expectedLike.User_ID);

            Assert.NotNull(actualLike);
        }


        [Fact]
        public void AddLike_Fail()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new LikeRepository(context);
            context.Likes.Add(expectedLike);
            context.SaveChanges();
            var like = repo.AddLike(expectedLike);


            Assert.Equal(String.Empty, like.Post_ID);
        }



        [Fact]
        public void RemoveLike_Success()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new LikeRepository(context);
            context.Likes.Add(expectedLike);
            context.SaveChanges();
            repo.RemoveLike(expectedLike.User_ID, expectedLike.Post_ID);


            Assert.Equal(0, context.Likes.Count());
        }

        [Fact]
        public void RemoveLike_Fail()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new LikeRepository(context);
            context.Likes.Add(expectedLike);
            context.SaveChanges();
            repo.RemoveLike(expectedLike.User_ID, "2");


            Assert.NotEqual(0, context.Likes.Count());
        }

        [Fact]
        public void GetLikeCount()
        {
            var expectedLike = GenFu.GenFu.New<Like>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var Post = GenFu.GenFu.New<Post>();
            expectedLike.Post_ID = Post.PostId.ToString();

            using var context = new AppDbContext(options);
            var repo = new LikeRepository(context);
            context.Posts.Add(Post);
            context.Likes.Add(expectedLike);
            context.SaveChanges();
            repo.GetLikeCount(Post.PostId.ToString());

            Assert.Equal(1, context.Likes.Count());
        }




    }
}
