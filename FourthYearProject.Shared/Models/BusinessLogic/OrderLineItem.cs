using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FourthYearProject.Shared.Models.BusinessLogic
{
    public class OrderLineItem
    {
        public OrderLineItem(Post p)
        {
            Post = p;
            Quantity = 1;
            Price = 00.00;
        }

        public OrderLineItem()
        {
        }


        [Key] public int Id { get; set; }

        public int PostId { get; set; }

        public string Type { get; set; }

        public string Size { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int? OrderId { get; set; }

        public Post Post { get; set; }

        public string GetItemTotal()
        {
            double Priceval = Price * Quantity;
#pragma warning disable S1121 // Assignments should not be made from within sub-expressions
            return Priceval.ToString("C", CultureInfo.CurrentCulture = new CultureInfo("en-IE"));
#pragma warning restore S1121 // Assignments should not be made from within sub-expressions
        }
    }
}