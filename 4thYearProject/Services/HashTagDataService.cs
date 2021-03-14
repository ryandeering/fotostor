using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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
                (await _httpClient.GetStreamAsync($"api/HashTag/{hashTag}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}

