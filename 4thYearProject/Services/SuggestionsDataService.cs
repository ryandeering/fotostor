using _4thYearProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _4thYearProject.Shared;

namespace _4thYearProject.Server.Services
{
    public class SuggestionsDataService : ISuggestionsDataService
    {

        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public SuggestionsDataService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<IEnumerable<Post>> GetSuggestions(string UserId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
                (await _httpClient.GetStreamAsync($"api/suggestions/{UserId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
