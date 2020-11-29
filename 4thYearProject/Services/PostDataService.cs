using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public class PostDataService : IPostDataService
    {
        private readonly HttpClient _httpClient;

        public PostDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Post> AddPost(Post post)
        {
            var postJson =
                new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/post", postJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Post>(await response.Content.ReadAsStreamAsync());
            }

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

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
                    (await _httpClient.GetStreamAsync($"api/post"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Post> GetPostDetails(int postId)
        {
            return await JsonSerializer.DeserializeAsync<Post>
                (await _httpClient.GetStreamAsync($"api/post/{postId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


    }
}
