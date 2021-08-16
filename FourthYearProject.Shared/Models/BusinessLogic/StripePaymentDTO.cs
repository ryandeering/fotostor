namespace _4thYearProject.Shared.Models.BusinessLogic
{
    public class StripePaymentDto
    {
        public long Amount { get; set; }
        public string ReturnUrl { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int CartId { get; set; }
        public string Email { get; set; }
    }
}