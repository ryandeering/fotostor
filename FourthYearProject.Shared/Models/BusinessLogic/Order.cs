using System;
using System.Collections.Generic;

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
        
        public Address OrderAddress { get; set; }
        
        public List<OrderLineItem> LineItems { get; set; }
    }
}