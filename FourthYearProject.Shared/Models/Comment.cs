using System;

namespace FourthYearProject.Shared.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public FeedProfileData ProfileData { get; set; }
        public DateTime SubmittedOn { get; set; }
    }
}