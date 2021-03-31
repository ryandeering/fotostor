namespace _4thYearProject.Server.Services
{
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class LikeDataService : ILikeDataService
    {
        private readonly IUserService _userService;

        private readonly HttpClient _httpClient;

        public LikeDataService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<Like> AddLike(Like like)
        {
            ClaimsPrincipal identity;
            identity = await _userService.GetUserAsync();


            //First get user claims    
            var claims = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault();

            //Filter specific claim    
            String UserId = claims.ToString();

            like.User_ID = UserId;

            var followingJson =
                new StringContent(JsonSerializer.Serialize(like), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/like", followingJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Like>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task RemoveLike(string Post_ID, string User_ID)
        {
            await _httpClient.DeleteAsync($"api/like/{Post_ID}/{User_ID}");
        }

        public async Task<bool> VerifyLike(string Post_ID, string User_ID)
        {

            var response = await _httpClient.GetAsync($"api/like/{Post_ID}/{User_ID}");

            if (response.IsSuccessStatusCode)

            {
                return true;

            }
            return false;
        }
    }
}
