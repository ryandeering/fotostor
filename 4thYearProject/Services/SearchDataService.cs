using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using _4thYearProject.Server.Services.Interfaces;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public class SearchDataService : ISearchDataService
{
    private readonly HttpClient _httpClient;

    public SearchDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<IEnumerable<SearchResult>> GetSearchResults(string searchResults)
    {

        var response = await _httpClient.GetAsync($"api/search/{searchResults}");

        if (response.IsSuccessStatusCode)
        {
            var result = await JsonSerializer.DeserializeAsync<IEnumerable<SearchResult>>(
                await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


            return result;

        }

        return new List<SearchResult>();
    }

    }
}
