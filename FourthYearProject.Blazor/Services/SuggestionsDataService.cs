using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models;

namespace FourthYearProject.Blazor.Services
{
    public class SuggestionsDataService : ISuggestionsDataService
    {
        private readonly HttpClient _httpClient;

        private readonly IUserService _userService;

        public SuggestionsDataService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<IEnumerable<Post>> GetSuggestions(string UserId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
            (await _httpClient.GetStreamAsync($"api/suggestions/{UserId}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}