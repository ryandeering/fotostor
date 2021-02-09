using Microsoft.AspNetCore.Identity;

namespace FourthYearProject.IDP.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] PhotoFile { get; set; }
    }
}
