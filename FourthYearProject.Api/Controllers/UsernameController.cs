using Microsoft.AspNetCore.Mvc;

namespace _4thYearProject.Api.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsernameController : Controller
    {
        private readonly IUserDataRepository _UserDataRepository;

        public UsernameController(IUserDataRepository UserDataRepository)
        {
            _UserDataRepository = UserDataRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserNameFromId(string id)
        {
            return Ok(_UserDataRepository.GetUserNameFromId(id));
        }
    }
}