using System;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestSupport.EfHelpers;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class PostRepositoryUnitTests
    {



        [Fact]
        public void GetAllFollowingPosts()
        {

           // var i = 3000;
           // GenFu.GenFu.Configure<Post>()
           //     .Fill(p => p.PostId, () => i++);
           // GenFu.GenFu.Configure<Following>().Fill(p => p.Followed_ID, "FOLLOWEDUSER2");
           // GenFu.GenFu.Configure<Post>().Fill(p => p.UserId, "FOLLOWEDUSER2");


           // var followings = GenFu.GenFu.ListOf<Following>(3);

           //var PostsActual = GenFu.GenFu.ListOf<Post>(2);
           //var options = SqliteInMemory.CreateOptions<AppDbContext>();
           //using var context = new AppDbContext(options);
           //context.Database.EnsureCreated();
           // foreach (var follow in followings) context.Followers.Add(follow);

           // foreach (var post in PostsActual) context.Posts.Add(post);

           // context.SaveChanges();


           // var repo = new PostRepository(context);

           // foreach (var follow in followings) {
           //     var posts = repo.GetAllPostsbyFollowing(follow.Follower_ID);
           //     Assert.NotNull(posts.OrderBy(p => p.UploadDate).First().Caption);
           // }
           // context.ChangeTracker.Clear();
           // context.Database.EnsureDeleted();
        }

        [Fact]
        public void GetPostByIdTest()
        {

            var PostsActual = GenFu.GenFu.New<Post>();
            PostsActual.PostId = 1001;


            var options = SqliteInMemory.CreateOptions<AppDbContext>();
            using var context = new AppDbContext(options);

            context.Database.EnsureCreated();
            context.Posts.Add(PostsActual);
            context.SaveChanges();
            var repo = new PostRepository(context);

            var PostObtained = repo.GetPostById(1001);
                Assert.Equal(PostObtained.Caption, PostsActual.Caption);


            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void UpdatePostTest_Success()
        {
            var Post = GenFu.GenFu.New<Post>();
            Post.PostId = 2908;


            var options = SqliteInMemory.CreateOptions<AppDbContext>();
            using var context = new AppDbContext(options);

            context.Database.EnsureCreated();
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = context.Posts.First();

            post.Caption = "An updated post.";

            var updatedPost = repo.UpdatePost(post);

            var updatedPost2 = context.Posts.First(p => p.Caption == "An updated post.");

            Assert.Equal(updatedPost.Caption, updatedPost2.Caption);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void UpdatePostTest_Fail()
        {
            var Post = GenFu.GenFu.New<Post>();


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = context.Posts.First();

            post.Caption = "An updated post.";
            post.PostId = 420;

            var updatedPost = repo.UpdatePost(post);


            Assert.Null(updatedPost);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void DeletePostTest_Success()
        {
            var Post = GenFu.GenFu.New<Post>();
            Post.PostId = 4839438;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);

            context.Database.EnsureCreated();
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            repo.DeletePost(context.Posts.First(p => p.Caption == Post.Caption).PostId);

            Assert.True(context.Posts.First(p => p.Caption == Post.Caption).PostDeleted);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void DeletePostTest_Fail()
        {
            var Post = GenFu.GenFu.New<Post>();


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = context.Posts.First();
            repo.DeletePost(post.PostId + 1);

            var updatedPost2 = repo.GetPostById(post.PostId);

            Assert.NotNull(updatedPost2);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void GetAllPosts()
        {
            var i = 500;
            GenFu.GenFu.Configure<Post>()
                .Fill(p => p.PostId, () => i++);

            var PostsActual = GenFu.GenFu.ListOf<Post>(3);


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            foreach (var Post in PostsActual) context.Posts.Add(Post);


            var posts = context.Posts;

            for (var j = 0; j < posts.Count(); j++)
                Assert.Equal(posts.ElementAt(j).Caption, PostsActual.ElementAt(j).Caption);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void GetPostsByUserId()
        {
            var i = 10;
            GenFu.GenFu.Configure<Post>()
                .Fill(p => p.PostId, () => i++);
            GenFu.GenFu.Configure<Post>().Fill(p => p.UserId, "MYSELF");

            var PostsActual = GenFu.GenFu.ListOf<Post>(2);

           var options = SqliteInMemory.CreateOptions<AppDbContext>();
           using var context = new AppDbContext(options);
           context.Database.EnsureCreated();
           foreach (var Post in PostsActual) context.Posts.Add(Post);
           var repo = new PostRepository(context);

           var posts = repo.GetPostsByUserId("MYSELF");

            Assert.NotNull(posts);

            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }


        [Fact]
        public void AddPost()
        {
            var PostActual = GenFu.GenFu.New<Post>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new PostRepository(context);

            repo.AddPost(PostActual);

            var post = context.Posts.First();

            Assert.Equal(PostActual.Caption, post.Caption);
            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
        }
    }
}