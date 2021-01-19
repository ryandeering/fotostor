using System;
using System.Collections.Generic;
using System.Text;

namespace _4thYearProject.Shared.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public string Body { get; set; }
        public DateTime SubmittedOn { get; set; }
        public Post Post { get; set; }
    }
}
