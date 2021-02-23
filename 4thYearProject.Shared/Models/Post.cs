using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;




namespace _4thYearProject.Shared.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public String UserId { get; set; }

        [MaxLength]
        public String PhotoFile { get; set; }

        [MaxLength]
        public String Thumbnail { get; set; }


        public String MimeType { get; set; }


        public bool LicenseEnabled { get; set; }
        public double LicensePrice { get; set; }
        public bool PrintsEnabled { get; set; }
        public bool ShirtsEnabled { get; set; }





        [Required]
        [StringLength(150, ErrorMessage = "Your caption exceeds 150 characters.")]
        public string Caption { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public int Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            UserId = String.Empty;
            PhotoFile = String.Empty;
            MimeType = String.Empty;
            Caption = String.Empty;
            UploadDate = DateTime.Now;
            Likes = 0;
            Comments = null;
        }


    }
}
