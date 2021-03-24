using System;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stripe;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class UnitTest1
    {

        [Fact]
        public void PostGetAllAsync()
        {

            var dbContextMock = new Mock<AppDbContext>();
            var dbSetMock = new Mock<DbSet<Post>>();
            dbContextMock.Setup(s => s.Set<Post>()).Returns(dbSetMock.Object);


            var postRepository = new PostRepository(dbContextMock.Object);


            var posts = postRepository.GetAllPosts();
            Assert.Single(posts);
        }



    }
}
