using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : Controller
    {
        private readonly IFollowingRepository _followingRepository;
        private readonly IWebHostEnvironment env;

        public FollowingController(IFollowingRepository followingRepository, IWebHostEnvironment env)
        {
            _followingRepository = followingRepository;
            this.env = env;
        }

        [HttpPost]
        public IActionResult AddFollowing([FromBody] Following following)
        {
            if (following == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFollowing = _followingRepository.AddFollowing(following);

            return Created("following", createdFollowing); // I don't think this is necessary. Look up the one for a button.

        }

        [HttpDelete("{Follower_ID}/{Followed_ID}")]
        public IActionResult RemoveFollowing(string Follower_ID, string Followed_ID)
        {
            if (Follower_ID == string.Empty ^ Followed_ID == string.Empty)
                return BadRequest();

            _followingRepository.RemoveFollowing(Follower_ID, Followed_ID);

            return NoContent();//success
        }


    }


}
