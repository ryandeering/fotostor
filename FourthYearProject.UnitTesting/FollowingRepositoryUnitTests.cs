using System.Linq;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class FollowingRepositoryUnitTests
    {
        [Fact]
        public void GetFollowingTest()
        {
            GenFu.GenFu.Configure<Following>().Fill(f => f.Follower_ID, "TESTID");

            var test1 = GenFu.GenFu.ListOf<Following>(3);


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Get Following")
                .Options;


            using var context = new AppDbContext(options);
            foreach (var following in test1) context.Followers.Add(following);
            context.SaveChanges();
            var repo = new FollowingRepository(context);
            var comments = repo.GetFollowing("TESTID");


            for (var j = 0; j < test1.Count; j++)
                Assert.Equal(test1[j].Follower_ID,
                    comments[j].Follower_ID);
        }

        [Fact]
        public void GetFollowersTest()
        {
            GenFu.GenFu.Configure<Following>().Fill(f => f.Followed_ID, "TESTID");

            var test1 = GenFu.GenFu.ListOf<Following>(3);


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Get Followers")
                .Options;


            using var context = new AppDbContext(options);
            foreach (var following in test1) context.Followers.Add(following);
            context.SaveChanges();
            var repo = new FollowingRepository(context);
            var comments = repo.GetFollowers("TESTID");


            for (var j = 0; j < test1.Count; j++)
                Assert.Equal(test1[j].Follower_ID,
                    comments[j].Follower_ID);
        }


        [Fact]
        public void RemoveFollowingTest()
        {
            GenFu.GenFu.Configure<Following>().Fill(f => f.Followed_ID, "TESTID");

            var test1 = GenFu.GenFu.ListOf<Following>(3);


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Remove Following")
                .Options;


            using var context = new AppDbContext(options);
            foreach (var following in test1) context.Followers.Add(following);
            context.SaveChanges();
            var repo = new FollowingRepository(context);
            repo.RemoveFollowing(test1[1].Follower_ID, test1[1].Followed_ID);
            Assert.Equal(2, context.Followers.Count());
        }

        [Fact]
        public void AddFollowingTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Add Following")
                .Options;


            using var context = new AppDbContext(options);
            var newFollow = new Following
            {
                Follower_ID = "hunter",
                Followed_ID = "prey"
            };
            var repo = new FollowingRepository(context);

            repo.AddFollowing(newFollow);
            Assert.Equal(1, context.Followers.Count());
        }

        [Fact]
        public void VerifyFollowingTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Verify Following")
                .Options;


            using var context = new AppDbContext(options);
            var newFollow = new Following
            {
                Follower_ID = "hunter",
                Followed_ID = "prey"
            };
            var repo = new FollowingRepository(context);

            repo.AddFollowing(newFollow);
            var verify = repo.VerifyFollowing("hunter", "prey");
            Assert.Equal("hunter", verify.Follower_ID);
        }
    }

}
