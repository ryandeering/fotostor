using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{
    public class Following
    {
        public int ID { get; set; }

        [Required] public string Follower_ID { get; set; }

        public string Followed_ID { get; set; }
        //why is it not updating in the migration?
    }
}