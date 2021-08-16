using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FourthYearProject.Blazor.Services;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models.BusinessLogic;
using Microsoft.AspNetCore.Components;

namespace FourthYearProject.Blazor.Pages
{
    public partial class Orders : ComponentBase
    {
        internal ClaimsPrincipal identity;


        private List<Order> orders;

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IShoppingCartService shoppingCartDataService { get; set; }

        [Inject] public NavigationManager UriHelper { get; set; }

        protected override async Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();


            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            orders = (await shoppingCartDataService.GetAllOrders(LoggedInID)).OrderByDescending(o => o.DatePlaced)
                .ToList();
        }


        private void Navigate(int OrderId)
        {
            UriHelper.NavigateTo("/orders/" + OrderId);
        }
    }
}