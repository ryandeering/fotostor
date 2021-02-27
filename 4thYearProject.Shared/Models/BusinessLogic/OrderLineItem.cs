namespace _4thYearProject.Shared.Models.BusinessLogic
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderLineItem
    {


        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int? OrderId { get; set; }

        public Post Post { get; set; }

        public OrderLineItem(Post p)
        {
            Post = p;
            Quantity = 1;
            Price = 69.00; //TODO, to be calculated.
        }

        public OrderLineItem()
        {
        }

        public double GetItemTotal()
        {
            return Price * Quantity;
        }
    }
}
