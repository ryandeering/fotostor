using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        private readonly IUserService _userService;

        private readonly IUserDataRepository _userDataRepository;

        public SuggestionsController(ISuggestionsRepository suggestionsRepository, IUserService userService, IUserDataRepository userDataRepository)
        {
            _suggestionsRepository = suggestionsRepository;
            _userService = userService;
            _userDataRepository = userDataRepository;
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
            }

            return Ok(Posts);
        }
    }
}