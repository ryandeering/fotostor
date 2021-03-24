using System;

namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Mvc;

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
