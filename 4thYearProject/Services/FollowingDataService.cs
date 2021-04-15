using _4thYearProject.Server.Pages;

namespace _4thYearProject.Server.Services
{
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

    public class FollowingDataService : IFollowingDataService
    {
        private readonly IUserService _userService;

        private readonly HttpClient _httpClient;

        public FollowingDataService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<Following> AddFollowing(Following follow)
        {
            ClaimsPrincipal identity;
            identity = await _userService.GetUserAsync();


            //First get user claims    
            var claims = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault();

            //Filter specific claim    
            String UserId = claims.ToString();

            follow.Follower_ID = UserId;

            var followingJson =
                new StringContent(JsonSerializer.Serialize(follow), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/following", followingJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Following>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<List<Following>> GetFollowers(string Following_ID)
        {
            var response = await _httpClient.GetAsync($"api/following/{Following_ID}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Following>>(await response.Content.ReadAsStreamAsync());
            }
            else { return null; }
        }

        public async Task<List<Following>> GetFollowing(string Follower_ID)
        {
            var response = await _httpClient.GetAsync($"api/following/fa/{Follower_ID}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<List<Following>>(await response.Content.ReadAsStreamAsync());
            }
            else { return null; }
        }

        public async Task RemoveFollowing(string Follower_ID, string Following_ID)
        {
            await _httpClient.DeleteAsync($"api/following/{Follower_ID}/{Following_ID}");
        }

        public async Task<Following> verifyFollowing(string Follower_ID, string Following_ID)
        {

            var response = await _httpClient.GetAsync($"api/following/{Follower_ID}/{Following_ID}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Following>(await response.Content.ReadAsStreamAsync());
            }
            else { return null; }
        }

        public async Task<List<FeedProfileData>> GetFollowingUserData(string Following_ID)
        {
            return await JsonSerializer.DeserializeAsync<List<FeedProfileData>>
                (await _httpClient.GetStreamAsync($"api/following/userdata/{Following_ID}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
