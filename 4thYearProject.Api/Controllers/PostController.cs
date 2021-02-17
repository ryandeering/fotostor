namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.CloudStorage;
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats;
    using SixLabors.ImageSharp.Processing;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        private readonly ICloudStorage _cloudStorage;

        private readonly IWebHostEnvironment env;

        public PostController(IPostRepository postRepository, IWebHostEnvironment env, ICloudStorage cloudStorage)
        {
            _postRepository = postRepository;
            this.env = env;
            _cloudStorage = cloudStorage;
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

        [HttpGet]
        [Route("following/{id}")]
        public IActionResult GetPostsbyFollowing(string id)
        {
            return Ok(_postRepository.GetAllPostsbyFollowing(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] Post post)
        {

            if (post == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            byte[] ImagetoUpload = Convert.FromBase64String(post.PhotoFile);
            byte[] Thumbnail;

            //https://stackoverflow.com/questions/55298428/how-to-resize-center-and-crop-an-image-with-imagesharp
            using (var inStream = new MemoryStream(ImagetoUpload))
            using (var outStream = new MemoryStream())
            using (var image = Image.Load(inStream, out IImageFormat format))
            {
                await image.SaveAsJpegAsync(outStream);

                ImagetoUpload = outStream.ToArray();


                image.Mutate(
                    i => i.Resize(110, 110));

                await image.SaveAsJpegAsync(outStream);


                Thumbnail = outStream.ToArray();

            }

            Random rand = new Random();
            int rand_num = rand.Next(100, 200);

            post.PhotoFile = await _cloudStorage.UploadFileAsync(ImagetoUpload, (post.UserId + "_" + post.PostId.ToString() + rand_num.ToString() + ".JPEG"));
            post.Thumbnail = await _cloudStorage.UploadFileAsync(Thumbnail, (post.UserId + "_" + post.PostId.ToString() + rand_num.ToString() + "T.JPEG"));

            var createdPost = _postRepository.AddPost(post);

            return Created("post", createdPost);
        }
    }
}
