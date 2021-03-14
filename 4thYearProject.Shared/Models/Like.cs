using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{
    public class Like
    {
        public int ID { get; set; }

        [Required] public string User_ID { get; set; }

        public string Post_ID { get; set; }
        //why is it not updating in the migration?
    }
}