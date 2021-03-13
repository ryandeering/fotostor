using _4thYearProject.Shared.Models;
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

        [HttpPost]
        public IActionResult GetUserNameFromId([FromBody] UsernameList list)
        {
            return Ok(_UserDataRepository.GetUserNameFromId(list));
        }
    }
}