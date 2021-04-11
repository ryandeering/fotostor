using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared;
using EllipticCurve.Utils;
using String = System.String;

namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;

        private readonly IWebHostEnvironment env;

        private readonly IUserService _userService;

        public LikeController(ILikeRepository likeRepository, IWebHostEnvironment env, IUserService userService)
        {
            _likeRepository = likeRepository;
            this.env = env;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] Like like)
        {

            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != like.User_ID)
                return Unauthorized();

            if (like == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdLike = _likeRepository.AddLike(like);

            return Created("like", createdLike);
        }

        [HttpDelete("{Post_ID}/{User_ID}")]
        public async Task<IActionResult> RemoveLike(string Post_ID, string User_ID)
        {

            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if ((Post_ID == string.Empty) ^ (User_ID == string.Empty))
                return BadRequest();

            _likeRepository.RemoveLike(User_ID, Post_ID);

            return NoContent(); //success
        }

        [HttpGet("{Post_ID}/{User_ID}")]
        public async Task<IActionResult> VerifyLike(string Post_ID, string User_ID)
        {

            if ((Post_ID == string.Empty) ^ (User_ID == string.Empty))
                return BadRequest();

            var IsLiked = _likeRepository.VerifyLike(Post_ID, User_ID);

            if (IsLiked == null)
                return NoContent();
            return Created("like", IsLiked);
        }
    }
}
