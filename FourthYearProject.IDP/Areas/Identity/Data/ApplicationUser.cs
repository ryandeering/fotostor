using System;
using Microsoft.AspNetCore.Identity;
using _4thYearProject.Shared;
namespace FourthYearProject.IDP.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] PhotoFile { get; set; }
    }
}
2