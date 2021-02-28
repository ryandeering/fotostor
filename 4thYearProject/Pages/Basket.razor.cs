namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class Basket : ComponentBase
    {

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public IShoppingCartService shoppingCartDataService { get; set; }


        public AuthenticationStateProvider _AuthenticationStateProvider { get; set; }

        public ShoppingCart basket = new ShoppingCart();

        public double price { get; set; }

        private ClaimsPrincipal identity { get; set; }

        [Parameter]
        public string LoggedInID { get; set; }

        protected async override Task OnInitializedAsync()
        {
            basket.basketItems = new List<OrderLineItem>();
            identity = await _userService.GetUserAsync();
            LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                   .Select(c => c.Value).SingleOrDefault().ToString();

            basket = await shoppingCartDataService.GetCart(LoggedInID);

            Task.Delay(1000);
        }

        protected override async Task OnParametersSetAsync()
        {


        }



        double getPrice()
        {
            foreach (var orderLineItem in basket.basketItems)
            {
                price += orderLineItem.Price;
            }

            return price;
        }


    }
}

