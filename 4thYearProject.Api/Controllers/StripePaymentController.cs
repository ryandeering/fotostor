using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SixLabors.ImageSharp;

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

        private readonly IUserService _userService;

        private readonly IWebHostEnvironment _environment;
        public StripePaymentController(IConfiguration configuration, IShoppingCartRepository shoppingCartRepository,
            IEmailSender emailSender, IUserDataRepository userDataRepository, IUserService userService, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _shoppingCartRepository = shoppingCartRepository;
            _emailSender = emailSender;
            _userDataRepository = userDataRepository;
            _userService = userService;
            _environment = environment;
        }

        [HttpGet("{UserId}/{session_id}")]
        public async Task<SuccessModel> OrderSuccess(string UserId, string session_id)
        {
            var identity = await _userService.GetUserAsync();

            if (identity == null)
                return new SuccessModel();

            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            if (LoggedInID != UserId)
                return new SuccessModel();


            var sessionService = new SessionService();
            var session = sessionService.Get(session_id);
            var OrderId = 0;
            var s = new SuccessModel();


            switch (session.PaymentStatus)
            {
                case "paid":

                    var temp = _shoppingCartRepository.PlaceOrder(UserId);
                    OrderId = (int)temp.OrderId;
                    s.OrderId = OrderId;


                    s.SuccessMessage = "Your order was a success. Your order ID is: " + temp.OrderId +
                                       " and you were charged " + string.Format("€{0:0.00}", session.AmountTotal / 100);

                    var rec = _userDataRepository.GetUserDataById(UserId);

                    var subject = "Order Number: #" + OrderId + " -- " + rec.DisplayName;

                    var order = _shoppingCartRepository.GetOrderById(OrderId);


                    var sb = new StringBuilder("");


                    sb.Append("<html>");
                    sb.Append("<head></head>");
                    sb.Append("<body>");
                    sb.Append(s.SuccessMessage);
                    sb.Append("</br>");

                    sb.Append("<h2>Address</h2>");
                    sb.Append(order.OrderAddress.UserFName + " " + order.OrderAddress.UserLName + "\n");
                    sb.AppendLine("<br>");
                    sb.Append(order.OrderAddress.UserAddress + "\n");
                    sb.AppendLine("<br>");
                    sb.Append(order.OrderAddress.UserAddress2 + "\n");
                    sb.AppendLine("<br>");
                    sb.Append(order.OrderAddress.UserCity + "\n");
                    sb.AppendLine("<br>");
                    sb.Append(order.OrderAddress.UserCountry + "\n");
                    sb.AppendLine("<br>");
                    sb.Append(order.OrderAddress.UserPostcode + "\n");
                    sb.AppendLine("<br>");


                    sb.Append("</br>");
                    sb.Append("<h2>Order Contents:</h2>");
                    sb.Append("<h3>Type | Quantity | Price | Details </h3>");
                    foreach (var olLineItem in order.LineItems)
                    {
                        String LicenseLink = String.Empty;

                        if (olLineItem.Type.Equals("License"))
                        {
                            LicenseLink = "<a href=" + olLineItem.Post.PhotoFile + ">License Link</a>";
                        }
                        sb.Append(olLineItem.Type + " | " + olLineItem.Quantity + " | " +
                                  $"€{olLineItem.Price:0.00}" + " | " +olLineItem.Size + LicenseLink);


                    }
                    sb.Append("</br>");
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
        public async Task<IActionResult> Create(StripePaymentDto payment)
        {
            try
            {
                string domain;
                if (_environment.IsDevelopment())
                {
                    domain = "https://localhost:44366";
                }
                else
                {
                    domain = "https://red-pebble-0ad568c03.azurestaticapps.net/";
                }
                
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
                                    Name = "‎‎‏‏‎ ‎‏‏‎ ‎"
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