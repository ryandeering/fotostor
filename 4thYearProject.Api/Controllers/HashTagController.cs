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

        public HashTagController(IHashTagRepository hashTagRepository)
        {
            _hashTagRepository = hashTagRepository;
        }

        [HttpGet("{hashTag}")]
        public IActionResult GetLatestPostsByHashTag(string hashTag)
        {
            return Ok(_hashTagRepository.GetLatestPostsByHashTag(hashTag));
        }

        [HttpPost("{hashTag}")]
        public IActionResult GetHashTag([FromBody] HashTag hashTag)
        {
            return Ok(_hashTagRepository.GetHashTag(hashTag.Content));
        }
    }
}