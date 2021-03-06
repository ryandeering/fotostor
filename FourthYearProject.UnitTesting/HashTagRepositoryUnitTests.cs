using System;
using System.Collections.Generic;
using FourthYearProject.Api.Models;
using FourthYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class HashTagRepositoryUnitTests
    {


        [Fact]
        public void GetLatestPostsByHashtag_Test()
        {

            var i = 6000;
            GenFu.GenFu.Configure<HashTag>()
                .Fill(p => p.Id, () => i++);


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
        }

        [Fact]
        public void GetHashTagSuccess_Test()
        {
            var i = 6100;
            GenFu.GenFu.Configure<HashTag>()
                .Fill(p => p.Id, () => i++);

            List<HashTag> hashTags = GenFu.GenFu.ListOf<HashTag>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            foreach (var Hashtag in hashTags) context.Hashtags.Add(Hashtag);
            context.SaveChanges();
            var repo = new HashTagRepository(context);

            foreach (var hashTagActual in hashTags)
            {
                Assert.Equal(hashTagActual.Content, repo.GetHashTag(hashTagActual.Content).Content);
            }

        }

        [Fact]
        public void GetHashTagNewHashtag_Test()
        {

            var i = 6200;
            GenFu.GenFu.Configure<HashTag>()
                .Fill(p => p.Id, () => i++);
            List<HashTag> hashTags = GenFu.GenFu.ListOf<HashTag>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            foreach (var Hashtag in hashTags) context.Hashtags.Add(Hashtag);
            context.SaveChanges();
            var repo = new HashTagRepository(context);

            foreach (var hashTagActual in hashTags)
            {
                Assert.Equal("dumdum",repo.GetHashTag("dumdum").Content);
            }

        }


    }
}
