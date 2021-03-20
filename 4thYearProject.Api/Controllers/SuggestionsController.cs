namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Shared.Models;
    using _4thYearProject.Api.Models;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsRepository _suggestionsRepository;

        public SuggestionsController(ISuggestionsRepository suggestionsRepository)
        {
            _suggestionsRepository = suggestionsRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetSuggestions(string id)
        {
            return Ok(_suggestionsRepository.GetSuggestions(id));
        }
    }
}
