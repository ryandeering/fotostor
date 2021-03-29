using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Api.CloudStorage;
using _4thYearProject.Api.Controllers;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using GenFu;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stripe;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class UnitTest1
    {



        Mock<IPostRepository> service = new Mock<IPostRepository>();
        Mock<ICloudStorage> service2 = new Mock<ICloudStorage>();
        Mock<IPostRepository> service3 = new Mock<IPostRepository>();
        Mock<IHashTagRepository> service4 = new Mock<IHashTagRepository>();
        Mock<IUserDataRepository> service5 = new Mock<IUserDataRepository>();
        Mock<IWebHostEnvironment> service6 = new Mock<IWebHostEnvironment>();



        private IEnumerable<Post> GetFakeData()
        {
            var i = 1;
            var persons = A.ListOf<Post>(26);
            persons.ForEach(x => x.PostId = i++);
            return persons.Select(_ => _);
        }


        [Fact]
        public async Task PostGetAllAsync()
        {
            var persons = GetFakeData();
            service.Setup(x => x.GetAllPosts()).Returns(persons);
            var controller = new PostController(service.Object, service4.Object, service6.Object, service2.Object, service5.Object);

            var results = controller.GetPosts();
            var okresult = results as OkObjectResult;
            var actualConfig = okresult.Value as IEnumerable<Post>;


            Assert.Equal(actualConfig.Count(), 26);

        }

        [Fact]
        public async Task PostGetByIdAsync()
        {
            var posts = GetFakeData();
            var fakePost = posts.First();
            service.Setup(x => x.GetPostById(1)).Returns(fakePost);
            var controller = new PostController(service.Object, service4.Object, service6.Object, service2.Object, service5.Object);

            var results = controller.GetPostbyId(1);
            var okresult = results as OkObjectResult;
            var actualPost = okresult.Value as Post;

            Assert.Equal(fakePost.Caption,actualPost.Caption);

        }

        [Fact]
        public async Task PostGetByUserNameAsync()
        {
            var posts = GetFakeData();
            var fakePost = posts.First();
            var postsofUsernamePosts = posts.Where(p => p.UserId.Equals(fakePost.UserId));
            service.Setup(x => x.GetPostsByUserId(fakePost.UserId)).Returns(postsofUsernamePosts);
            var controller = new PostController(service.Object, service4.Object, service6.Object, service2.Object, service5.Object);

            var results = controller.GetPostsByUserId(fakePost.UserId);
            var okresult = results as OkObjectResult;
            var actualPost = okresult.Value as IEnumerable<Post>;

            Assert.Equal(postsofUsernamePosts.ToList(), actualPost.ToList());

        }
















    }
}
