using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace _4thYearProject.Shared.Models.BusinessLogic
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
            return Priceval.ToString("C", CultureInfo.CurrentCulture = new CultureInfo("en-IE"));
        }
    }
}