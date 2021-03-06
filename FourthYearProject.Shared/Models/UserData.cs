using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FourthYearProject.Shared.Models
{
    public class UserData
    {
        public string Id { get; set; }

        [Required] public string DisplayName { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public Address Address { get; set; }
    }

}