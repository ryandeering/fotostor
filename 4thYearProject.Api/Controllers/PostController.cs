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
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IWebHostEnvironment env;

        public CommentController(ICommentRepository commentRepository, IWebHostEnvironment env)
        {
            _commentRepository = commentRepository;
            this.env = env;
        }


        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetCommentsbyPostId(int id)
        {
            return Ok(_commentRepository.GetCommentsByPostId(id));
        }

        [HttpGet]
        [Route("specific/{id}")]
        public IActionResult GetCommentById(int Comment_Id)
        {
            return Ok(_commentRepository.GetCommentById(Comment_Id));
        }

        [HttpPost]
        public IActionResult CreateCommentAsync([FromBody] Comment comment )
        {

            if (comment == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var createdComment = _commentRepository.AddComment(comment);

            return Created("comment", createdComment);
 
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            if (id == 0)
                return BadRequest();

            var employeeToDelete = _commentRepository.GetCommentById(id);
            if (employeeToDelete == null)
                return NotFound();

            _commentRepository.DeleteComment(id);

            return NoContent();//success
        }


    [HttpPut]
    public IActionResult UpdateComment([FromBody] Comment comment)
    {
        if (comment == null)
            return BadRequest();

        //if (comment.FirstNam == string.Empty || comment.LastName == string.Empty)
        //{
        //    ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
        //}

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var employeeToUpdate = _commentRepository.GetCommentById(comment.Id);

        if (employeeToUpdate == null)
            return NotFound();

        _commentRepository.UpdateComment(comment);

        return NoContent(); //success
    }


}


}
