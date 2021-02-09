using _4thYearProject.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public UserService(AuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<ClaimsPrincipal> GetUserAsync()
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }




    }
}