using System;
using System.Collections.Generic;
using System.Text;

namespace _4thYearProject.Shared.Models.BusinessLogic
{
    public class Order
    {
        public Order()
        {
            LineItems = new List<OrderLineItem>();
        }


        public int? OrderId { get; set; }

        public string UserId { get; set; }

        public DateTime? DatePlaced { get; set; }

        public string UserName { get; set; }

        public string UserAddress { get; set; }

        public string UserCity { get; set; }

        public string UserCountry { get; set; }

        public List<OrderLineItem> LineItems { get; set; }



    }
}
