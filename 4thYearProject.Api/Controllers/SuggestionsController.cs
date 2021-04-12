using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        private readonly IUserService _userService;

        public SuggestionsController(ISuggestionsRepository suggestionsRepository, IUserService userService)
        {
            _suggestionsRepository = suggestionsRepository;
            _userService = userService;
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


            return Ok(_suggestionsRepository.GetSuggestions(id));
        }
    }
}