using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserData> AddUserData(UserData User)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(User), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/userdata", userJson);

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<UserData>(await response.Content.ReadAsStreamAsync());

            return null;
        }

        public async Task<FeedProfileData> GetUserNameFromId(string id)
        {
            var response = await _httpClient.GetAsync($"api/username/{id}");

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<FeedProfileData>(
                    await response.Content.ReadAsStreamAsync());

            return null;
        }


        public async Task UpdateUserData(UserData User)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(User), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/userdata", userJson);
        }

        public async Task DeleteUserData(string Id)
        {
            await _httpClient.DeleteAsync($"api/userdata/{Id}");
        }

        public async Task<IEnumerable<UserData>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UserData>>
            (await _httpClient.GetStreamAsync("api/userdata"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task<UserData> GetUserDataDetails(string Id)
        {
            return await JsonSerializer.DeserializeAsync<UserData>
            (await _httpClient.GetStreamAsync($"api/userdata/{Id}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task<UserData> GetUserDataDetailsInFull(string Id)
        {
            return await JsonSerializer.DeserializeAsync<UserData>
            (await _httpClient.GetStreamAsync($"api/userdata/full/{Id}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }


        public async Task<UserData> GetUserDataDetailsByDisplayName(string DisplayName)
        {
            return await JsonSerializer.DeserializeAsync<UserData>
            (await _httpClient.GetStreamAsync($"api/userdata/displayname/{DisplayName}"),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}