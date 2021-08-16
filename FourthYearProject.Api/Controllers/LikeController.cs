using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;

        private readonly IUserService _userService;

        public LikeController(ILikeRepository likeRepository, IUserService userService)
        {
            _likeRepository = likeRepository;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] Like like)
        {
            var identity = await _userService.GetUserAsync();

            if (like == null)
                return BadRequest();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != like.User_ID)
                return Unauthorized();


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

            if ((Post_ID == string.Empty) ^ (User_ID == string.Empty))
                return BadRequest();

            _likeRepository.RemoveLike(User_ID, Post_ID);

            return NoContent(); //success
        }

        [HttpGet("{Post_ID}/{User_ID}")]
        public IActionResult VerifyLike(string Post_ID, string User_ID)
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