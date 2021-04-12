using System.Linq;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class PostRepositoryUnitTests
    {

        [Fact]
        public void GetAllPostsTest()
        {
            var test1 = GenFu.GenFu.New<Post>();


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Products Test")
                .Options;


            using var context = new AppDbContext(options);
            context.Posts.Add(test1);
            context.SaveChanges();
            var repo = new PostRepository(context);
            var posts = repo.GetAllPosts();

            Assert.Equal(posts.First().Caption, test1.Caption);
        }

        [Fact]
        public void GetAllFollowingPosts()
        {
            GenFu.GenFu.Configure<Following>().Fill(p => p.Followed_ID, "FOLLOWEDUSER");
            GenFu.GenFu.Configure<Post>().Fill(p => p.UserId, "FOLLOWEDUSER");


            var followings = GenFu.GenFu.ListOf<Following>(3);

            var PostsActual = GenFu.GenFu.ListOf<Post>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Following Test")
                .Options;


            using var context = new AppDbContext(options);
            foreach (var follow in followings) context.Followers.Add(follow);

            foreach (var post in PostsActual) context.Posts.Add(post);

            context.SaveChanges();


            var repo = new PostRepository(context);

            foreach (var follow in followings)
            {
                var posts = repo.GetAllPostsbyFollowing(follow.Follower_ID);
                Assert.Equal(posts.OrderByDescending(p => p.UploadDate).First().Caption,
                    PostsActual.OrderByDescending(p => p.UploadDate).First().Caption);
            }
        }

        [Fact]
        public void GetPostByIdTest()
        {
            var i = 1;
            GenFu.GenFu.Configure<Post>()
                .Fill(p => p.PostId, () => i++);

            var PostsActual = GenFu.GenFu.ListOf<Post>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Posts Test")
                .Options;

            using var context = new AppDbContext(options);
            foreach (var Post in PostsActual) context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var posts = repo.GetAllPosts();

            for (var j = 1; j < PostsActual.Count; j++)
            {
                var PostObtained = repo.GetPostById(j);
                Assert.Equal(PostObtained.Caption, posts.First(p => p.PostId == j).Caption);
            }
        }

        [Fact]
        public void UpdatePostTest_Success()
        {
            var Post = GenFu.GenFu.New<Post>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Posts Update Test")
                .Options;

            using var context = new AppDbContext(options);
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = repo.GetAllPosts().First();

            post.Caption = "An updated post.";

            var updatedPost = repo.UpdatePost(post);

            var updatedPost2 = repo.GetAllPosts().First();

            Assert.Equal(updatedPost.Caption, updatedPost2.Caption);
        }


        [Fact]
        public void UpdatePostTest_Fail()
        {
            var Post = GenFu.GenFu.New<Post>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Posts Update Test")
                .Options;

            using var context = new AppDbContext(options);
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = repo.GetAllPosts().First();

            post.Caption = "An updated post.";
            post.PostId = 420;

            var updatedPost = repo.UpdatePost(post);


            Assert.Null(updatedPost);
        }

        [Fact]
        public void DeletePostTest_Success()
        {
            var Post = GenFu.GenFu.New<Post>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Posts Delete Fail Test")
                .Options;

            using var context = new AppDbContext(options);
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = repo.GetAllPosts().First();
            repo.DeletePost(post.PostId);

            var updatedPost2 = repo.GetPostById(post.PostId);

            Assert.Null(updatedPost2);
        }


        [Fact]
        public void DeletePostTest_Fail()
        {
            var Post = GenFu.GenFu.New<Post>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Posts Update Test")
                .Options;

            using var context = new AppDbContext(options);
            context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var post = repo.GetAllPosts().First();
            repo.DeletePost(post.PostId + 1);

            var updatedPost2 = repo.GetPostById(post.PostId);

            Assert.NotNull(updatedPost2);
        }


        [Fact]
        public void GetAllPosts()
        {
            var i = 1;
            GenFu.GenFu.Configure<Post>()
                .Fill(p => p.PostId, () => i++);

            var PostsActual = GenFu.GenFu.ListOf<Post>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("All Posts Test")
                .Options;

            using var context = new AppDbContext(options);
            foreach (var Post in PostsActual) context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var posts = repo.GetAllPosts();

            for (var j = 0; j < posts.Count(); j++)
                Assert.Equal(posts.ElementAt(j).Caption, PostsActual.ElementAt(j).Caption);
        }


        [Fact]
        public void GetPostsByUserId()
        {
            var i = 1;
            GenFu.GenFu.Configure<Post>()
                .Fill(p => p.PostId, () => i++);
            GenFu.GenFu.Configure<Post>().Fill(p => p.UserId, "MYSELF");


            var PostsActual = GenFu.GenFu.ListOf<Post>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("All Posts By User Id Test")
                .Options;

            using var context = new AppDbContext(options);
            foreach (var Post in PostsActual) context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new PostRepository(context);


            var posts = repo.GetPostsByUserId("MYSELF");

            for (var j = 0; j < posts.Count(); j++)
                Assert.Equal(posts.OrderByDescending(p => p.UploadDate).ElementAt(j).Caption,
                    PostsActual.OrderByDescending(p => p.UploadDate).ElementAt(j).Caption);
        }


        [Fact]
        public void AddPost()
        {
            var PostActual = GenFu.GenFu.New<Post>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Posts Add Test")
                .Options;

            using var context = new AppDbContext(options);
            var repo = new PostRepository(context);

            repo.AddPost(PostActual);

            var post = repo.GetAllPosts().First();

            Assert.Equal(PostActual.Caption, post.Caption);
        }
    }
}