using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using _4thYearProject.Shared;
namespace _4thYearProject.Api.Controllers.Identity
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