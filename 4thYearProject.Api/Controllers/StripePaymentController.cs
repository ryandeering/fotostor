using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared;
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
            int? OrderId = 0;
            var s = new SuccessModel();


            switch (session.PaymentStatus)
            {
                case "paid":

                    var temp = _shoppingCartRepository.PlaceOrder(UserId);
                    OrderId = temp.OrderId;

                    foreach (var ol in temp.LineItems)
                        s.SuccessMessage += "<tr><td>" + "<img>" + ol.Post.Thumbnail + "</img>" + "</td><td>" +
                                            ol.Quantity + "</td><td> " + ol.Price + " Euro</td><td>" + ol.OrderId +
                                            " Euro</td></tr>";

                    var rec = _userDataRepository.GetUserDataById(UserId);

                    var subject = "Order Number: #" + OrderId + " -- " + rec.DisplayName;
                    var body = "/* Base ------------" + "------------------ *" + "/ @import url(\"http" +
                               "s://fonts.googleapis" + ".com/css?family=Nuni" + "to+Sans:400,700&disp" +
                               "lay=swap\"); body { " + "width: 100% !importa" + "nt; height: 100%; ma" +
                               "rgin: 0; -webkit-tex" + "t-size-adjust: none;" + " } a { color: #3869D" +
                               "4; } a img { border:" + " none; } td { word-b" + "reak: break-word; } " +
                               ".preheader { display" + ": none !important; v" + "isibility: hidden; m" +
                               "so-hide: all; font-s" + "ize: 1px; line-heigh" + "t: 1px; max-height: " +
                               "0; max-width: 0; opa" + "city: 0; overflow: h" + "idden; } /* Type ---" +
                               "--------------------" + "------- */ body, td," + " th { font-family: " +
                               "\"Nunito Sans\", Hel" + "vetica, Arial, sans-" + "serif; } h1 { margin" +
                               "-top: 0; color: #333" + "333; font-size: 22px" + "; font-weight: bold;" +
                               " text-align: left; }" + " h2 { margin-top: 0;" + " color: #333333; fon" +
                               "t-size: 16px; font-w" + "eight: bold; text-al" + "ign: left; } h3 { ma" +
                               "rgin-top: 0; color: " + "#333333; font-size: " + "14px; font-weight: b" +
                               "old; text-align: lef" + "t; } td, th { font-s" + "ize: 16px; } p, ul, " +
                               "ol, blockquote { mar" + "gin: .4em 0 1.1875em" + "; font-size: 16px; l" +
                               "ine-height: 1.625; }" + " p.sub { font-size: " + "13px; } /* Utilities" +
                               " -------------------" + "----------- */ .alig" + "n-right { text-align" +
                               ": right; } .align-le" + "ft { text-align: lef" + "t; } .align-center {" +
                               " text-align: center;" + " } /* Buttons ------" + "--------------------" +
                               "---- */ .button { ba" + "ckground-color: #386" + "9D4; border-top: 10p" +
                               "x solid #3869D4; bor" + "der-right: 18px soli" + "d #3869D4; border-bo" +
                               "ttom: 10px solid #38" + "69D4; border-left: 1" + "8px solid #3869D4; d" +
                               "isplay: inline-block" + "; color: #FFF; text-" + "decoration: none; bo" +
                               "rder-radius: 3px; bo" + "x-shadow: 0 2px 3px " + "rgba(0, 0, 0, 0.16);" +
                               " -webkit-text-size-a" + "djust: none; box-siz" + "ing: border-box; } ." +
                               "button--green { back" + "ground-color: #22BC6" + "6; border-top: 10px " +
                               "solid #22BC66; borde" + "r-right: 18px solid " + "#22BC66; border-bott" +
                               "om: 10px solid #22BC" + "66; border-left: 18p" + "x solid #22BC66; } ." +
                               "button--red { backgr" + "ound-color: #FF6136;" + " border-top: 10px so" +
                               "lid #FF6136; border-" + "right: 18px solid #F" + "F6136; border-bottom" +
                               ": 10px solid #FF6136" + "; border-left: 18px " + "solid #FF6136; } @me" +
                               "dia only screen and " + "(max-width: 500px) {" + " .button { width: 10" +
                               "0% !important; text-" + "align: center !impor" + "tant; } } /* Attribu" +
                               "te list ------------" + "------------------ *" + "/ .attributes { marg" +
                               "in: 0 0 21px; } .att" + "ributes_content { ba" + "ckground-color: #F4F" +
                               "4F7; padding: 16px; " + "} .attributes_item {" + " padding: 0; } /* Re" +
                               "lated Items --------" + "--------------------" + "-- */ .related { wid" +
                               "th: 100%; margin: 0;" + " padding: 25px 0 0 0" + "; -premailer-width: " +
                               "100%; -premailer-cel" + "lpadding: 0; -premai" + "ler-cellspacing: 0; " +
                               "} .related_item { pa" + "dding: 10px 0; color" + ": #CBCCCF; font-size" +
                               ": 15px; line-height:" + " 18px; } .related_it" + "em-title { display: " +
                               "block; margin: .5em " + "0 0; } .related_item" + "-thumb { display: bl" +
                               "ock; padding-bottom:" + " 10px; } .related_he" + "ading { border-top: " +
                               "1px solid #CBCCCF; t" + "ext-align: center; p" + "adding: 25px 0 10px;" +
                               " } /* Discount Code " + "--------------------" + "---------- */ .disco" +
                               "unt { width: 100%; m" + "argin: 0; padding: 2" + "4px; -premailer-widt" +
                               "h: 100%; -premailer-" + "cellpadding: 0; -pre" + "mailer-cellspacing: " +
                               "0; background-color:" + " #F4F4F7; border: 2p" + "x dashed #CBCCCF; } " +
                               ".discount_heading { " + "text-align: center; " + "} .discount_body { t" +
                               "ext-align: center; f" + "ont-size: 15px; } /*" + " Social Icons ------" +
                               "--------------------" + "---- */ .social { wi" + "dth: auto; } .social" +
                               " td { padding: 0; wi" + "dth: auto; } .social" + "_icon { height: 20px" +
                               "; margin: 0 8px 10px" + " 8px; padding: 0; } " + "/* Data table ------" +
                               "--------------------" + "---- */ .purchase { " + "width: 100%; margin:" +
                               " 0; padding: 35px 0;" + " -premailer-width: 1" + "00%; -premailer-cell" +
                               "padding: 0; -premail" + "er-cellspacing: 0; }" + " .purchase_content {" +
                               " width: 100%; margin" + ": 0; padding: 25px 0" + " 0 0; -premailer-wid" +
                               "th: 100%; -premailer" + "-cellpadding: 0; -pr" + "emailer-cellspacing:" +
                               " 0; } .purchase_item" + " { padding: 10px 0; " + "color: #51545E; font" +
                               "-size: 15px; line-he" + "ight: 18px; } .purch" + "ase_heading { paddin" +
                               "g-bottom: 8px; borde" + "r-bottom: 1px solid " + "#EAEAEC; } .purchase" +
                               "_heading p { margin:" + " 0; color: #85878E; " + "font-size: 12px; } ." +
                               "purchase_footer { pa" + "dding-top: 15px; bor" + "der-top: 1px solid #" +
                               "EAEAEC; } .purchase_" + "total { margin: 0; t" + "ext-align: right; fo" +
                               "nt-weight: bold; col" + "or: #333333; } .purc" + "hase_total--label { " +
                               "padding: 0 15px 0 0;" + " } body { background" + "-color: #F4F4F7; col" +
                               "or: #51545E; } p { c" + "olor: #51545E; } p.s" + "ub { color: #6B6E76;" +
                               " } .email-wrapper { " + "width: 100%; margin:" + " 0; padding: 0; -pre" +
                               "mailer-width: 100%; " + "-premailer-cellpaddi" + "ng: 0; -premailer-ce" +
                               "llspacing: 0; backgr" + "ound-color: #F4F4F7;" + " } .email-content { " +
                               "width: 100%; margin:" + " 0; padding: 0; -pre" + "mailer-width: 100%; " +
                               "-premailer-cellpaddi" + "ng: 0; -premailer-ce" + "llspacing: 0; } /* M" +
                               "asthead ------------" + "----------- */ .emai" + "l-masthead { padding" +
                               ": 25px 0; text-align" + ": center; } .email-m" + "asthead_logo { width" +
                               ": 94px; } .email-mas" + "thead_name { font-si" + "ze: 16px; font-weigh" +
                               "t: bold; color: #A8A" + "AAF; text-decoration" + ": none; text-shadow:" +
                               " 0 1px 0 white; } /*" + " Body --------------" + "---------------- */ " +
                               ".email-body { width:" + " 100%; margin: 0; pa" + "dding: 0; -premailer" +
                               "-width: 100%; -prema" + "iler-cellpadding: 0;" + " -premailer-cellspac" +
                               "ing: 0; background-c" + "olor: #FFFFFF; } .em" + "ail-body_inner { wid" +
                               "th: 570px; margin: 0" + " auto; padding: 0; -" + "premailer-width: 570" +
                               "px; -premailer-cellp" + "adding: 0; -premaile" + "r-cellspacing: 0; ba" +
                               "ckground-color: #FFF" + "FFF; } .email-footer" + " { width: 570px; mar" +
                               "gin: 0 auto; padding" + ": 0; -premailer-widt" + "h: 570px; -premailer" +
                               "-cellpadding: 0; -pr" + "emailer-cellspacing:" + " 0; text-align: cent" +
                               "er; } .email-footer " + "p { color: #6B6E76; " + "} .body-action { wid" +
                               "th: 100%; margin: 30" + "px auto; padding: 0;" + " -premailer-width: 1" +
                               "00%; -premailer-cell" + "padding: 0; -premail" + "er-cellspacing: 0; t" +
                               "ext-align: center; }" + " .body-sub { margin-" + "top: 25px; padding-t" +
                               "op: 25px; border-top" + ": 1px solid #EAEAEC;" + " } .content-cell { p" +
                               "adding: 35px; } /*Me" + "dia Queries --------" + "--------------------" +
                               "-- */ @media only sc" + "reen and (max-width:" + " 600px) { .email-bod" +
                               "y_inner, .email-foot" + "er { width: 100% !im" + "portant; } } @media " +
                               "(prefers-color-schem" + "e: dark) { body, .em" + "ail-body, .email-bod" +
                               "y_inner, .email-cont" + "ent, .email-wrapper," + " .email-masthead, .e" +
                               "mail-footer { backgr" + "ound-color: #333333 " + "!important; color: #" +
                               "FFF !important; } p," + " ul, ol, blockquote," + " h1, h2, h3, span, ." +
                               "purchase_item { colo" + "r: #FFF !important; " + "} .attributes_conten" +
                               "t, .discount { backg" + "round-color: #222 !i" + "mportant; } .email-m" +
                               "asthead_name { text-" + "shadow: none !import" + "ant; } } :root { col" +
                               "or-scheme: light dar" + "k; supported-color-s" + "chemes: light dark; " +
                               "} This is a receipt " + "for your recent purc" + "hase on {{ purchase_" +
                               "date }}. No payment " + "is due with this rec" + "eipt. [Product Name]" +
                               " Hi {{name}}, Thanks" + " for using [Product " + "Name]. This email is" +
                               " the receipt for you" + "r purchase. No payme" + "nt is due. This purc" +
                               "hase will appear as " + "“[Credit Card Statem" + "ent Name]” on your c" +
                               "redit card statement" + " for your {{credit_c" + "ard_brand}} ending i" +
                               "n {{credit_card_last" + "_four}}. Need to upd" + "ate your payment inf" +
                               "ormation? 10% off yo" + "ur next purchase! Th" + "anks for your suppor" +
                               "t! Here\'s a coupon " + "for 10% off your nex" + "t purchase if used b" +
                               "y {{expiration_date}" + "}. Use this discount" + " now... {{receipt_id" +
                               "}} {{date}} {{#each " + "receipt_details}} {{" + "/each}} Description " +
                               "Amount {{description" + "}} {{amount}} Total " + "{{total}} If you hav" +
                               "e any questions abou" + "t this receipt, simp" + "ly reply to this ema" +
                               "il or reach out to o" + "ur support team for " + "help. Cheers, The [P" +
                               "roduct Name] Team Do" + "wnload as PDF Need a" + " printable copy for " +
                               "your records? You ca" + "n download a PDF ver" + "sion. Moved recently" +
                               "? Have a new credit " + "card? You can easily" + " update your billing" +
                               " information. © 2021" + " [Product Name]. All" + " rights reserved. [C" +
                               "ompany Name, LLC] 12" + "34 Street Rd. Suite " + "1234";

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

            s.SuccessMessage +=
                $"<html><body><h1>Order Id: {OrderId} Thanks for your order: {detail}!</h1></body></html>";

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