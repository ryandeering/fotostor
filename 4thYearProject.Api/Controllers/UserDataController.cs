using _4thYearProject.Api.CloudStorage;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : Controller
    {
        private readonly ICloudStorage _cloudStorage;

        private readonly IUserDataRepository _UserDataRepository;

        private readonly IUserService _userService;

        public UserDataController(IUserDataRepository UserDataRepository, ICloudStorage cloudStorage,
            IUserService userService)
        {
            _UserDataRepository = UserDataRepository;
            _cloudStorage = cloudStorage;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserDatas()
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            return Ok(_UserDataRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDataById(string id)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            return Ok(_UserDataRepository.GetUserDataById(id));
        }

        [HttpGet("full/{id}")]
        public async Task<IActionResult> GetUserDataInFull(string id)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != id)
                return Unauthorized();


            return Ok(_UserDataRepository.GetUserDataInFull(id));
        }


        [HttpGet("displayname/{DisplayName}")]
        public async Task<IActionResult> GetUserDataByDisplayName(string DisplayName)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            return Ok(_UserDataRepository.GetUserDataByDisplayName(DisplayName));
        }

        [HttpPost]
        public IActionResult CreateUserData([FromBody] UserData UserData)
        {
            if (UserData == null)
                return null;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUserData = _UserDataRepository.AddUserData(UserData);

            return Created("UserData", createdUserData);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserData([FromBody] UserData UserData)
        {
            if (UserData == null)
                return BadRequest();

            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserData.Id)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UserDataToUpdate = _UserDataRepository.GetUserDataById(UserData.Id);

            if (UserDataToUpdate == null)
                return NotFound();

            if (UserData.ProfilePic != UserDataToUpdate.ProfilePic)
            {
                var ImagetoUpload = Convert.FromBase64String(UserData.ProfilePic);


                try
                {
                    using (var inStream = new MemoryStream(ImagetoUpload))
                    using (var outStream = new MemoryStream())
                    using (var image = Image.Load(inStream, out var format))
                    {
                        image.Mutate(
                            i => i.Resize(250, 250));

                        await image.SaveAsJpegAsync(outStream);


                        ImagetoUpload = outStream.ToArray();
                    }

                    var rand = new Random();
                    var rand_num = rand.Next(100, 200);
                    UserData.ProfilePic =
                        await _cloudStorage.UploadFileAsync(ImagetoUpload, UserData.Id + rand_num + ".JPEG");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            _UserDataRepository.UpdateUserData(UserData);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserData(string id)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != id)
                return Unauthorized();

            if ((id == string.Empty) ^ (id == null))
                return BadRequest();

            var UserDataToDelete = _UserDataRepository.GetUserDataById(id);
            if (UserDataToDelete == null)
                return NotFound();

            _UserDataRepository.DeleteUserData(id);

            return NoContent(); //success
        }
    }
}