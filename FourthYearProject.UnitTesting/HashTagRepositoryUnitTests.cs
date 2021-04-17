using System;
using System.Collections.Generic;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class HashTagRepositoryUnitTests
    {


        [Fact]
        public void GetFollowingTest()
        {

            List<HashTag> hashTags = GenFu.GenFu.ListOf<HashTag>(5);

            GenFu.GenFu.Configure<Post>().Fill(p => p.HashTags, hashTags);

            var Posts = GenFu.GenFu.ListOf<Post>(5);


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;


            using var context = new AppDbContext(options);
            foreach (var Post in Posts) context.Posts.Add(Post);
            context.SaveChanges();
            var repo = new HashTagRepository(context);



            foreach (var Post in Posts)
            {
               var collection = repo.GetLatestPostsByHashTag(hashTags.First().Content);

               Assert.Contains(Post, collection);
            }

            context.ChangeTracker.Clear();
            context.Database.EnsureDeleted();
            context.Dispose();
        }



    }
}
