using System.ComponentModel.DataAnnotations;

namespace FourthYearProject.Shared.Models
{
    public class Following
    {
        public int ID { get; set; }

        [Required] public string Follower_ID { get; set; }

        public string Followed_ID { get; set; }
    }
}