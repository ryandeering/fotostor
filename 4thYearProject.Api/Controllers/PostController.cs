using _4thYearProject.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using _4thYearProject.Shared.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using System.IO;
using SixLabors.ImageSharp.Processing;
using ImageMagick;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IWebHostEnvironment env;

        public PostController(IPostRepository postRepository, IWebHostEnvironment env)
        {
            _postRepository = postRepository;
            this.env = env;
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

        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetPostsByUserId(string id)
        {
            return Ok(_postRepository.GetPostsByUserId(id));
        }


        [HttpPost]
        public IActionResult CreatePostAsync([FromBody] Post post )
        {


            //https://stackoverflow.com/questions/55298428/how-to-resize-center-and-crop-an-image-with-imagesharp


            using (var inStream = new MemoryStream(post.PhotoFile))
            using (var outStream = new MemoryStream())
            using (var image = Image.Load(inStream, out IImageFormat format))
            {
                image.Mutate(
                    i => i.Resize(110, 110));

                image.Save(outStream, format);
                

                post.Thumbnail = outStream.ToArray();
            }







            if (post == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);




                

            var createdPost = _postRepository.AddPost(post);

            return Created("post", createdPost);


 
        }


    }


}
