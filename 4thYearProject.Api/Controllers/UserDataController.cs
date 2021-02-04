using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : Controller
    {
        private readonly IUserDataRepository _UserDataRepository;

        public UserDataController(IUserDataRepository UserDataRepository)
        {
            _UserDataRepository = UserDataRepository;
        }

        [HttpGet]
        public IActionResult GetAllUserDatas()
        {
            return Ok(_UserDataRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserDataById(string id)
        {
            return Ok(_UserDataRepository.GetUserDataById(id));
        }

        [HttpGet("displayname/{DisplayName}")]
        public IActionResult GetUserDataByDisplayName(string DisplayName)
        {
            return Ok(_UserDataRepository.GetUserDataByDisplayName(DisplayName));
        }


        [HttpPost]
        public IActionResult CreateUserData(UserData UserData)
        {
            if (UserData == null)
                return BadRequest();

            //if (UserData.FirstName == string.Empty || UserData.LastName == string.Empty)
            //{
            //    ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUserData = _UserDataRepository.AddUserData(UserData);

            return Created("UserData", createdUserData);
        }

        [HttpPut]
        public IActionResult UpdateUserData([FromBody] UserData UserData)
        {
            if (UserData == null)
                return BadRequest();

            //if (UserData.FirstName == string.Empty || UserData.LastName == string.Empty)
            //{
            //    ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UserDataToUpdate = _UserDataRepository.GetUserDataById(UserData.Id);

            if (UserDataToUpdate == null)
                return NotFound();

            _UserDataRepository.UpdateUserData(UserData);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserData(string id)
        {
            if (id == String.Empty ^ id == null)
                return BadRequest();

            var UserDataToDelete = _UserDataRepository.GetUserDataById(id);
            if (UserDataToDelete == null)
                return NotFound();

            _UserDataRepository.DeleteUserData(id);

            return NoContent();//success
        }
    }
}
