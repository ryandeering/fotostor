using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{
    public class UserData
    {
        public string Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfilePic { get; set; }
    }

    public class UsernameList
    {
        public List<string> ListofUsernames { get; set; }
    }

}
