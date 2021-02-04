﻿namespace FourthYearProject.IDP.Areas.Identity.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
           // identity.AddClaim(new Claim("PhotoFile", user.DisplayName));
            return identity;
        }
    }
}
