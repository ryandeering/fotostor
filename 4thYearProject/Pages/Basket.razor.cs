using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _4thYearProject.Server.Services;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using _4thYearProject.Shared.Models.BusinessLogic;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace _4thYearProject.Server.Pages
{
    public partial class Basket : ComponentBase
    {
        private ShoppingCart basket = new();

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IShoppingCartService shoppingCartDataService { get; set; }

        [Inject] public IStripePaymentService stripePaymentService { get; set; }

        [Inject] public IJSRuntime jsRuntime { get; set; }

        [Inject] public IMatToaster Toaster { get; set; }

        public AuthenticationStateProvider _AuthenticationStateProvider { get; set; }

        public double price { get; set; }

        private ClaimsPrincipal identity { get; set; }

        private UserData user { get; set; }

        [Parameter] public string LoggedInID { get; set; }

        public string Email { get; set; }

        protected override async Task OnInitializedAsync()
        {
            price = 0;
            basket.BasketItems = new List<OrderLineItem>();
            identity = await _userService.GetUserAsync();
            if (identity.Identity.IsAuthenticated)
            {
                LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value).SingleOrDefault().ToString();

                Email = identity.Claims.Where(c => c.Type.Equals("email"))
                    .Select(c => c.Value).SingleOrDefault().ToString();

                basket = await shoppingCartDataService.GetCart(LoggedInID);

                user = await UserDataService.GetUserDataDetailsInFull(LoggedInID);
            }
        }

        private async Task PlaceOrder()
        {
            if (user.Address == null)
            {
                Toaster.Add("You have no address set. Please make sure you set one.", MatToastType.Danger, "FAILURE");
                return;
            }

            var order = new StripePaymentDto
            {
                CartId = basket.Id,
                UserId = LoggedInID,
                Amount = ConvertEuroToCents(price),
                Email = Email
            };


            var result = await stripePaymentService.CheckOut(order);


            await jsRuntime.InvokeVoidAsync("redirectToCheckout", result.Data.ToString());
        }

        private async Task EmptyBasket()
        {
            await shoppingCartDataService.EmptyBasket(LoggedInID);
            basket = await shoppingCartDataService.GetCart(LoggedInID);
            Toaster.Add("Your basket has been emptied.", MatToastType.Success, "SUCCESS");
        }

        private async Task RemoveOne(OrderLineItem ol)
        {
            await shoppingCartDataService.RemoveOne(LoggedInID, ol);
            basket = await shoppingCartDataService.GetCart(LoggedInID);
            Toaster.Add("Item removed.", MatToastType.Success, "SUCCESS");
        }

        private async Task AddOne(OrderLineItem ol)
        {
            await shoppingCartDataService.AddOne(LoggedInID, ol);
            basket = await shoppingCartDataService.GetCart(LoggedInID);
            Toaster.Add("Item added.", MatToastType.Success, "SUCCESS");
        }


        public static int ConvertEuroToCents(double euros)
        {
            return (int) (euros * 100);
        }

        internal string getPrice()
        {
            price = 0.0;
            foreach (var orderLineItem in basket.BasketItems) price += orderLineItem.Price * orderLineItem.Quantity;

            return price.ToString("C", CultureInfo.CurrentCulture = new CultureInfo("en-IE"));
        }
    }
}