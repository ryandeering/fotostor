using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models
{
    public class Post
    {
        public Post()
        {
            UserId = string.Empty;
            PhotoFile = string.Empty;
            MimeType = string.Empty;
            Caption = string.Empty;
            UploadDate = DateTime.Now;
            Likes = 0;
            Comments = null;
            HashTags = new List<HashTag>();
        }

        public int PostId { get; set; }

        public string UserId { get; set; }

        [MaxLength] public string PhotoFile { get; set; }

        [MaxLength] public string Thumbnail { get; set; }


        public FeedProfileData ProfileData { get; set; }

        public string MimeType { get; set; }
        public Boolean Liked { get; set; }


        public bool LicenseEnabled { get; set; }
        public double LicensePrice { get; set; }
        public bool PrintsEnabled { get; set; }
        public bool ShirtsEnabled { get; set; }
        public bool PostDeleted { get; set; }


        [Required]
        [StringLength(150, ErrorMessage = "Your caption exceeds 150 characters.")]
        public string Caption { get; set; }

        [Required] public DateTime UploadDate { get; set; }

        [Required] public int Likes { get; set; }

       
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<HashTag> HashTags { get; set; }
    }
}