using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using _4thYearProject.Shared.Models;

namespace FourthYearProject.IDP.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public String DisplayName { get; set; }

        public String ProfilePicURL { get; set; }
    }
}
