namespace _4thYearProject.Shared.Models.BusinessLogic
{
    public abstract class ProductType
    {
        public int Id { get; set; }

        public virtual double Price => 0;
    }

    public class License : ProductType
    {
    }

    public class Print : ProductType
    {
        public string PrintSize { get; set; }
    }

    public class Shirt : ProductType
    {
        public string ShirtSize { get; set; }
    }
}
