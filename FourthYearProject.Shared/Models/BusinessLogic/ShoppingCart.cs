using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _4thYearProject.Shared.Models.BusinessLogic
{
    public class ShoppingCart
    {
        [Required] public int Id { get; set; }

        public List<OrderLineItem> BasketItems { get; set; }

        public string UserId { get; set; }
    }
}