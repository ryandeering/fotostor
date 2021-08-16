using System.Linq;
using System.Threading.Tasks;
using FourthYearProject.Api.Models;
using FourthYearProject.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FourthYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : Controller
    {
        private readonly ILikeRepository _likeRepository;
        private readonly ISuggestionsRepository _suggestionsRepository;

        private readonly IUserDataRepository _userDataRepository;

        private readonly IUserService _userService;

        public SuggestionsController(ISuggestionsRepository suggestionsRepository, IUserService userService,
            IUserDataRepository userDataRepository, ILikeRepository likeRepository)
        {
            _suggestionsRepository = suggestionsRepository;
            _userService = userService;
            _userDataRepository = userDataRepository;
            _likeRepository = likeRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuggestions(string id)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != id)
                return Unauthorized();

            var Posts = _suggestionsRepository.GetSuggestions(id);
            foreach (var Post in Posts)
            {
                Post.ProfileData = _userDataRepository.GetUserNameFromId(Post.UserId);
                Post.Likes = _likeRepository.GetLikeCount(Post.PostId.ToString());
            }

            return Ok(Posts);
        }
    }
}