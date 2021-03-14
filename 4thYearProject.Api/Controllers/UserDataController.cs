namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.CloudStorage;
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Mvc;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : Controller
    {
        private readonly ICloudStorage _cloudStorage;

        private readonly IUserDataRepository _UserDataRepository;

        public UserDataController(IUserDataRepository UserDataRepository, ICloudStorage cloudStorage)
        {
            _UserDataRepository = UserDataRepository;
            _cloudStorage = cloudStorage;
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
        public IActionResult CreateUserData([FromBody] UserData UserData)
        {
            if (UserData == null)
                return null;

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
        public async Task<IActionResult> UpdateUserData([FromBody] UserData UserData)
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

                        image.SaveAsJpegAsync(outStream);


                        ImagetoUpload = outStream.ToArray();
                    }

                    var rand = new Random();
                    var rand_num = rand.Next(100, 200);
                    UserData.ProfilePic =
                        await _cloudStorage.UploadFileAsync(ImagetoUpload, UserData.Id + rand_num + ".JPEG");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); //todo: move this logic to repository
                }
            }


            _UserDataRepository.UpdateUserData(UserData);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserData(string id)
        {
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
