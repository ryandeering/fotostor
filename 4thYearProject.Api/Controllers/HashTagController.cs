namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.Models;
    using Microsoft.AspNetCore.Mvc;

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
    }
}