using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _4thYearProject.Shared.Models
{

    public class ProfilePic
    {
        public string Id { get; set; }
        [Required]
        public byte[] PhotoFile { get; set; }
    }
}
