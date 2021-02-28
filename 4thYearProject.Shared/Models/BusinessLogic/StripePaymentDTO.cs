using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4thYearProject.Shared
{
    public class StripePaymentDTO
    {
        public long Amount { get; set; }
        public string ReturnUrl { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int CartId { get; set; }
    }
}
