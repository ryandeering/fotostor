using System;
using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{
    public class Like
    {
        public int ID { get; set; }

        [Required] public string User_ID { get; set; }

        public string Post_ID { get; set; }
        //why is it not updating in the migration?


        public Like(string User_ID, string Post_ID)
        {
            this.User_ID = User_ID;
            this.Post_ID = Post_ID;
        }

        public Like()
        {
            this.User_ID = String.Empty;
            this.Post_ID = String.Empty;
        }



    }
}