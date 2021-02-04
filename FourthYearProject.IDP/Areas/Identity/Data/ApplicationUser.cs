using Microsoft.AspNetCore.Identity;
using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace FourthYearProject.IDP.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] PhotoFile { get; set; }
    }
}
