using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok(_postRepository.GetAllPosts());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetPostbyId(int id)
        {
            return Ok(_postRepository.GetPostById(id));
        }


        [HttpPost]
        public IActionResult CreatePost([FromBody] Post post)
        {
            if (post == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdPost = _postRepository.AddPost(post);

            return Created("post", createdPost);
        }
    }
}
