using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FourthYearProject.Api.CloudStorage;
using FourthYearProject.Api.Models;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace FourthYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly ICloudStorage _cloudStorage;

        private readonly IHashTagRepository _hashTagRepository;
        private readonly ILikeRepository _likeRepository;

        private readonly IPostRepository _postRepository;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IUserService _userService;


        public PostController(IPostRepository postRepository, IHashTagRepository hashTagRepository,
            ICloudStorage cloudStorage, IUserDataRepository userDataRepository,
            IUserService userService, ILikeRepository likeRepository)
        {
            _postRepository = postRepository;
            _hashTagRepository = hashTagRepository;
            _cloudStorage = cloudStorage;
            _userDataRepository = userDataRepository;
            _userService = userService;
            _likeRepository = likeRepository;
        }


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetPostbyId(int id)
        {
            var Post = _postRepository.GetPostById(id);

            if (Post == null) return NotFound();


            Post.Likes = _likeRepository.GetLikeCount(id.ToString());
            Post.ProfileData = _userDataRepository.GetUserNameFromId(Post.UserId);
            return Ok(Post);
        }

        [HttpGet]
        [Route("user/{id}")]
        public IActionResult GetPostsByUserId(string id)
        {
            return Ok(_postRepository.GetPostsByUserId(id));
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            var identity = await _userService.GetUserAsync();
            var post = _postRepository.GetPostById(int.Parse(postId));


            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (post == null)
                return NotFound();


            if (LoggedInID != post.UserId)
                return Unauthorized();

            _postRepository.DeletePost(int.Parse(postId));
            return NoContent();
        }


        [HttpGet]
        [Route("following/{id}")]
        public IActionResult GetPostsbyFollowing(string id)
        {
            var Posts = _postRepository.GetAllPostsbyFollowing(id);

            foreach (var Post in Posts)
            {
                Post.ProfileData = _userDataRepository.GetUserNameFromId(Post.UserId);
                Post.Likes = _likeRepository.GetLikeCount(Post.PostId.ToString());
            } 
            return Ok(Posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] Post post)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != post.UserId)
                return Unauthorized();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var ImagetoUpload = Convert.FromBase64String(post.PhotoFile);
            byte[] Thumbnail;

            //https://stackoverflow.com/questions/55298428/how-to-resize-center-and-crop-an-image-with-imagesharp
            using (var inStream = new MemoryStream(ImagetoUpload))
            using (var outStream = new MemoryStream())
            using (var image = Image.Load(inStream, out var format))
            {
                await image.SaveAsJpegAsync(outStream);

                ImagetoUpload = outStream.ToArray();


                image.Mutate(
                    i => i.Resize(110, 110));

                await image.SaveAsJpegAsync(outStream);


                Thumbnail = outStream.ToArray();
            }

            var rand = new Random();
            var rand_num = rand.Next(100, 200);

            post.PhotoFile =
                await _cloudStorage.UploadFileAsync(ImagetoUpload,
                    post.UserId + "_" + post.PostId + rand_num + ".JPEG");
            post.Thumbnail =
                await _cloudStorage.UploadFileAsync(Thumbnail, post.UserId + "_" + post.PostId + rand_num + "T.JPEG");


            var hashTags = Regex.Matches(post.Caption, @"\#\w*").Select(m => m.Value).ToArray();
            foreach (var hashTag in hashTags)
            {
                var hashTagResult = _hashTagRepository.GetHashTag(hashTag);
                post.HashTags.Add(hashTagResult);
            }

            var createdPost = _postRepository.AddPost(post);

            return Created("post", createdPost);
        }
    }
}