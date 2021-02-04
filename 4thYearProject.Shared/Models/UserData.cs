using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _4thYearProject.Shared.Models
{
    public class UserData
    {
        public string Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public ProfilePic Pic { get; set; }
    }
}
