using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourthYearProject.Api.Models;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FourthYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : Controller
    {
        private readonly IFollowingRepository _followingRepository;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IUserService _userService;


        public FollowingController(IFollowingRepository followingRepository,
            IUserService userService, IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
            _followingRepository = followingRepository;
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

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
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

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
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

        [HttpGet("userdata/{Follower_ID}")]
        public IActionResult GetFollowersUserData(string Follower_ID)
        {
            if (Follower_ID == string.Empty)
                return BadRequest();

            var FollowingList = _followingRepository.GetFollowers(Follower_ID);

            var profileDatas = new List<FeedProfileData>();

            if (FollowingList == null)
                return BadRequest();

            foreach (var Following in FollowingList)
                profileDatas.Add(_userDataRepository.GetUserNameFromId(Following.Follower_ID));

            return Ok(profileDatas);
        }
    }
}