using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;



namespace _4thYearProject.Shared.Models
{
  public class Post
    {
        public int PostId { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string ThumbnailURL { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Your caption exceeds 150 characters.")]
        public string Caption { get; set; }

        public DateTime UploadDate{ get; set; }

        public int Likes { get; set; }

    }
}
