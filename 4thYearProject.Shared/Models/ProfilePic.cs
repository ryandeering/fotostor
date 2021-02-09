using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{

    public class ProfilePic
    {
        public string Id { get; set; }
        [Required]
        public byte[] PhotoFile { get; set; }
    }
}
