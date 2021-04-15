using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{
    public class FeedProfileData
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string ProfilePicURL { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
