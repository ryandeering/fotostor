using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared;

namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IWebHostEnvironment env;

        private readonly IUserService _userService;

        public CommentController(ICommentRepository commentRepository, IWebHostEnvironment env, IUserService userService)
        {
            _commentRepository = commentRepository;
            this.env = env;
            _userService = userService;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetCommentsByPostId(int id)
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
        public async Task<IActionResult> CreateCommentAsync(Comment comment)
        {

            if (comment == null)
                return BadRequest();

            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != comment.UserId)
                return Unauthorized();

        

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var createdComment = _commentRepository.AddComment(comment);

            return Created("comment", createdComment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            if (id == 0)
                return BadRequest();

            var commentToDelete = _commentRepository.GetCommentById(id);

            if (commentToDelete == null)
                return NotFound();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != commentToDelete.UserId)
                return Unauthorized();

   

            _commentRepository.DeleteComment(id);

            return NoContent(); //success
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] Comment comment)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return Unauthorized();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != comment.UserId)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var commentToUpdate = _commentRepository.GetCommentById(comment.Id);

            if (commentToUpdate == null)
                return NotFound();

            _commentRepository.UpdateComment(comment);

            return NoContent(); //success
        }
    }
}
