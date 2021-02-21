using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _4thYearProject.Shared.Models.BusinessLogic
{
    public class ShoppingCart
    {
        [Required]
        public int Id { get; set; }

        public List<OrderLineItem> basketItems { get; set; }

        public string UserId { get; set; }





    }
}
