using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public class HashTagDataService : IHashTagDataService
    {
        private readonly HttpClient _httpClient;

        public HashTagDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Post>> GetLatestPostsByHashTag(string hashTag)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
            (await _httpClient.GetStreamAsync($"api/HashTag/{hashTag}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task<HashTag> GetHashTag(HashTag hashTag)
        {
            var hashTagJson =
                new StringContent(JsonSerializer.Serialize(hashTag), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/hashtag", hashTagJson);

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<HashTag>(await response.Content.ReadAsStreamAsync());

            return null;
        }
    }
}