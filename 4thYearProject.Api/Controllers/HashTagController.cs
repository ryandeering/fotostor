using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashTagController : Controller
    {
        private readonly IHashTagRepository _hashTagRepository;

        private readonly IUserDataRepository _userDataRepository;

        public HashTagController(IHashTagRepository hashTagRepository, IUserDataRepository userDataRepository)
        {
            _hashTagRepository = hashTagRepository;
            _userDataRepository = userDataRepository;
        }

        [HttpGet("{hashTag}")]
        public IActionResult GetLatestPostsByHashTag(string hashTag)
        {
            var Posts = _hashTagRepository.GetLatestPostsByHashTag(hashTag);

            foreach (var Post in Posts)
            {
                Post.ProfileData = _userDataRepository.GetUserNameFromId(Post.UserId);
            }

            return Ok(Posts);
        }

        [HttpPost("{hashTag}")]
        public IActionResult GetHashTag([FromBody] HashTag hashTag)
        {
            return Ok(_hashTagRepository.GetHashTag(hashTag.Content));
        }
    }
}