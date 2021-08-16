using System.Collections.Generic;

namespace FourthYearProject.Shared.Models
{
    public class HashTag
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Post> Posts { get; set; }


    }
}
