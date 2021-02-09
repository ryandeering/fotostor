using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;




namespace _4thYearProject.Shared.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string UserId { get; set; }

        [MaxLength]
        public byte[] PhotoFile { get; set; }

        [MaxLength]
        public byte[] Thumbnail { get; set; }


        public string MimeType { get; set; }


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
            PhotoFile = null;
            MimeType = String.Empty;
            Caption = String.Empty;
            UploadDate = DateTime.Now;
            Likes = 0;
            Comments = null;
        }


    }
}
