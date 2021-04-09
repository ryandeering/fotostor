using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared;

namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : Controller
    {
        private readonly IFollowingRepository _followingRepository;

        private readonly IWebHostEnvironment env;
        private readonly IUserService _userService;

        public FollowingController(IFollowingRepository followingRepository, IWebHostEnvironment env, IUserService userService)
        {
            _followingRepository = followingRepository;
            this.env = env;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFollowing([FromBody] Following following)
        {
            if (following == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != following.Follower_ID)
                return Unauthorized();

            var createdFollowing = _followingRepository.AddFollowing(following);

            return
                Created("following",
                    createdFollowing);
        }

        [HttpDelete("{Follower_ID}/{Followed_ID}")]
        public async Task<IActionResult> RemoveFollowing(string Follower_ID, string Followed_ID)
        {
            if ((Follower_ID == string.Empty) ^ (Followed_ID == string.Empty))
                return BadRequest();

            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != Follower_ID)
                return Unauthorized();

            _followingRepository.RemoveFollowing(Follower_ID, Followed_ID);

            return NoContent(); //success
        }

        [HttpGet("{Follower_ID}/{Followed_ID}")]
        public IActionResult VerifyFollowing(string Follower_ID, string Followed_ID)
        {
            if ((Follower_ID == string.Empty) ^ (Followed_ID == string.Empty))
                return BadRequest();

            var IsFollowing = _followingRepository.VerifyFollowing(Follower_ID, Followed_ID);

            if (IsFollowing == null)
                return BadRequest();
            return Created("following", IsFollowing);
        }

        [HttpGet("{Followed_ID}")]
        public IActionResult GetFollowers(string Followed_ID)
        {
            if (Followed_ID == string.Empty)
                return BadRequest();

            var FollowingList = _followingRepository.GetFollowers(Followed_ID);

            if (FollowingList == null)
                return BadRequest();
            return Created("following", FollowingList);
        }

        [HttpGet("fa/{Follower_ID}")]
        public IActionResult GetFollowing(string Follower_ID)
        {
            if (Follower_ID == string.Empty)
                return BadRequest();

            var FollowingList = _followingRepository.GetFollowing(Follower_ID);

            if (FollowingList == null)
                return BadRequest();
            return Created("following", FollowingList);
        }
    }
}
