using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace _4thYearProject.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IEmailSender _emailSender;

        private readonly IShoppingCartRepository _shoppingCartRepository;

        private readonly IUserDataRepository _userDataRepository;

        public StripePaymentController(IConfiguration configuration, IShoppingCartRepository shoppingCartRepository,
            IEmailSender emailSender, IUserDataRepository userDataRepository)
        {
            _configuration = configuration;
            _shoppingCartRepository = shoppingCartRepository;
            _emailSender = emailSender;
            _userDataRepository = userDataRepository;
        }

        [HttpGet("{UserId}/{session_id}")]
        public async Task<SuccessModel> OrderSuccess(string UserId, string session_id)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(session_id);
            var detail = string.Empty;
            var OrderId = 0;
            var s = new SuccessModel();


            switch (session.PaymentStatus)
            {
                case "paid":

                    var temp = _shoppingCartRepository.PlaceOrder(UserId);
                    OrderId = (int) temp.OrderId;
                    s.OrderId = OrderId;


                    s.SuccessMessage = "Your order was a success. Your order ID is: " + temp.OrderId +
                                       " and you were charged " + string.Format("€{0:0.00}", session.AmountTotal / 100);

                    var rec = _userDataRepository.GetUserDataById(UserId);

                    var subject = "Order Number: #" + OrderId + " -- " + rec.DisplayName;

                    var sb = new StringBuilder("");


                    sb.Append("<html>");
                    sb.Append("<head></head>");
                    sb.Append("<body>");
                    sb.Append(s.SuccessMessage);
                    sb.Append("</body>");
                    sb.Append("</html>");


                    await _emailSender.SendEmailAsync(rec.Email, subject, sb.ToString());


                    break;

                case "unpaid":
                    break;
            }

            return s;
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
                        new()
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = payment.Amount, //convert to cents
                                Currency = "eur",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "sample"
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    CustomerEmail = payment.Email,
                    SuccessUrl = domain + "/success-payment?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = domain + payment.ReturnUrl
                };

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                return Ok(new SuccessModel
                {
                    Data = session.Id
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorModel
                {
                    ErrorMessage = e.Message
                });
            }
        }
    }
}