using _4thYearProject.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using _4thYearProject.Shared.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using System.IO;
using SixLabors.ImageSharp.Processing;
using ImageMagick;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IWebHostEnvironment env;

        public LikeController(ILikeRepository likeRepository, IWebHostEnvironment env)
        {
            _likeRepository = likeRepository;
            this.env = env;
        }

        [HttpPost]
        public IActionResult AddLike([FromBody] Like like)
        {
            if (like == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdLike = _likeRepository.AddLike(like);

            return Created("like", createdLike); // I don't think this is necessary. Look up the one for a button.
 
        }

        [HttpDelete("{Post_ID}/{User_ID}")]
        public IActionResult RemoveLike(string Post_ID, string User_ID)
        {
            if (Post_ID == string.Empty ^ User_ID == string.Empty)
                return BadRequest();

            _likeRepository.RemoveLike(Post_ID, User_ID);

            return NoContent();//success
        }


    }


}
