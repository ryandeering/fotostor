﻿using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IUserService _userService;

        private readonly IUserDataRepository _userDataRepository;

        private readonly IPostRepository _postRepository;

        private readonly IWebHostEnvironment env;

        public CommentController(ICommentRepository commentRepository, IWebHostEnvironment env,
            IUserService userService, IUserDataRepository userDataRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            this.env = env;
            _userService = userService;
            _userDataRepository = userDataRepository;
            _postRepository = postRepository;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetCommentsByPostId(int id)
        {
            var Comments = _commentRepository.GetCommentsByPostId(id);

            if (Comments == null)
            {
                return NotFound();
            }

            foreach (var Comment in Comments) Comment.ProfileData =_userDataRepository.GetUserNameFromId(Comment.UserId);

            return Ok(Comments);
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

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
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

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != commentToDelete.UserId || LoggedInID == _postRepository.GetPostById(commentToDelete.PostId).UserId) 
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

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
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