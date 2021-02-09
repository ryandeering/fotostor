using _4thYearProject.Api.Models;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class UnitTest1
    {

#pragma warning disable S3459 // Unassigned members should be removed
        private readonly IPostRepository _postRepository;
#pragma warning restore S3459 // Unassigned members should be removed


        [Fact]
        public void PostGetAllAsync()
        {
            var posts = _postRepository.GetAllPosts(); // add mock authentication
            Assert.Single(posts);
        }



    }
}
