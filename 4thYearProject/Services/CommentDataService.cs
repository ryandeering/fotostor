using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public class CommentDataService : ICommentDataService
    {

        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public CommentDataService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            ClaimsPrincipal identity;
            identity = await _userService.GetUserAsync();


            //First get user claims    
            var claims = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault();

            //Filter specific claim    
            String UserId = claims.ToString();

            comment.UserId = UserId;
            comment.SubmittedOn = DateTime.Now;

            var commentJson =
                new StringContent(JsonSerializer.Serialize(comment), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/comment", commentJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Comment>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateComment(Comment comment)
        {
            var commentJson =
                new StringContent(JsonSerializer.Serialize(comment), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/post", commentJson);
        }

        public async Task DeleteComment(int Comment_ID)
        {
            await _httpClient.DeleteAsync($"api/comment/{Comment_ID}");
        }



        public async Task<IEnumerable<Comment>> GetCommentsByPostId(int id)
        {

            return await JsonSerializer.DeserializeAsync<IEnumerable<Comment>>
                (await _httpClient.GetStreamAsync($"api/comment/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }

        public async Task<Comment> GetCommentById(int Comment_ID)
        {

            return await JsonSerializer.DeserializeAsync<Comment>
                (await _httpClient.GetStreamAsync($"api/comment/{Comment_ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }


    }
}
