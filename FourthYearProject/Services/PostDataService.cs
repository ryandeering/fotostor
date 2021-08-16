using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public class PostDataService : IPostDataService
    {
        private readonly HttpClient _httpClient;

        private readonly IUserService _userService;

        public PostDataService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<Post> AddPost(Post post)
        {
            ClaimsPrincipal identity;
            identity = await _userService.GetUserAsync();


            //First get user claims    
            var claims = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault();

            //Filter specific claim    
            var UserId = claims;

            post.UserId = UserId;
            post.UploadDate = DateTime.Now;

            var postJson =
                new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/post", postJson);

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<Post>(await response.Content.ReadAsStreamAsync());

            return null;
        }

        public async Task UpdatePost(Post post)
        {
            var postJson =
                new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/post", postJson);
        }

        public async Task DeletePost(int postId)
        {
            await _httpClient.DeleteAsync($"api/post/{postId}");
        }


        public async Task<Post> GetPostDetails(int postId)
        {
            return await JsonSerializer.DeserializeAsync<Post>
            (await _httpClient.GetStreamAsync($"api/post/{postId}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task<IEnumerable<Post>> GetAllPostsbyFollowing(string id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
            (await _httpClient.GetStreamAsync($"api/post/following/{id}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(string id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
            (await _httpClient.GetStreamAsync($"api/post/user/{id}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}