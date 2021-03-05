namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class OrderDetails : ComponentBase
    {

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public IShoppingCartService shoppingCartDataService { get; set; }

        [Inject]
        public NavigationManager UriHelper { get; set; }

        [Parameter]
        public string OrderId { get; set; }

        private Order Order;

        public double TotalPrice { get; set; }
        internal ClaimsPrincipal identity;

        protected async override Task OnInitializedAsync()
        {

            //TODO validate user is owner
            identity = await _userService.GetUserAsync();


            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault().ToString();


                Order = (await shoppingCartDataService.GetOrderById(Int32.Parse(OrderId)));
        }


        void Navigate()
        {
            UriHelper.NavigateTo("/orders/");
        }
    }
}

