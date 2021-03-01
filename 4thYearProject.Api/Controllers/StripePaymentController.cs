namespace _4thYearProject.Api.Controllers
{
    using _4thYearProject.Api.Models;
    using _4thYearProject.Shared;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe.Checkout;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IShoppingCartRepository _shoppingCartRepository;

        public StripePaymentController(IConfiguration configuration, IShoppingCartRepository shoppingCartRepository)
        {
            _configuration = configuration;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("{UserId}/{session_id}")]
        public ContentResult OrderSuccess(string UserId, string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);
            string detail = String.Empty;
            int? OrderId = 0;

            switch (session.PaymentStatus)
            {
                case "paid":

                    var temp = _shoppingCartRepository.PlaceOrder(UserId);
                    OrderId = temp.OrderId;

                    foreach (var ol in temp.LineItems)
                    {

                        detail += "<tr><td>" + "<img>" + ol.Post.Thumbnail + "</img>" + "</td><td>" + ol.Quantity.ToString() + "</td><td> " + ol.Price.ToString() + " Euro</td><td>" + ol.OrderId.ToString() + " Euro</td></tr>";


                    }

                    break;

                case "unpaid":
                    break;
            }



            return Content($"<html><body><h1>Order Id: {OrderId} Thanks for your order: {detail}!</h1></body></html>");
        }

        [HttpPost]
        public async Task<IActionResult> Create(StripePaymentDTO payment)
        {
            try
            {
                var domain = "https://localhost:44366";

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount=payment.Amount,//convert to cents
                                Currency="eur",
                                ProductData= new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "sample"
                                }
                            },
                            Quantity=1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = domain + "/success-payment?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = domain + payment.ReturnUrl
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return Ok(new SuccessModel()
                {
                    Data = session.Id
                });

            }
            catch (Exception e)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
