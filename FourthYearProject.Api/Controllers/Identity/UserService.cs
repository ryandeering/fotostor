using System.Security.Claims;
using System.Threading.Tasks;
using FourthYearProject.Shared;
using Microsoft.AspNetCore.Http;

namespace FourthYearProject.Api.Controllers.Identity
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public Task<ClaimsPrincipal> GetUserAsync()
        {
            return Task.FromResult(accessor.HttpContext.User);
        }
    }
}