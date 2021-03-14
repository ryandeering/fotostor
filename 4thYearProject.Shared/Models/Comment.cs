using System;

namespace _4thYearProject.Shared.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public DateTime SubmittedOn { get; set; }
    }
}