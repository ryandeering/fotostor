﻿namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class Orders : ComponentBase
    {

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public IShoppingCartService shoppingCartDataService { get; set; }

        [Inject]
        public NavigationManager UriHelper { get; set; }


        private List<Order> orders;

        internal ClaimsPrincipal identity;

        protected async override Task OnInitializedAsync()
        {


            identity = await _userService.GetUserAsync();


            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault().ToString();

            orders = (await shoppingCartDataService.GetAllOrders(LoggedInID)).OrderByDescending(o => o.DatePlaced).ToList();
   
        }


        void Navigate(int OrderId)
        {
            UriHelper.NavigateTo("/orders/" + OrderId);
        }

    }
}

