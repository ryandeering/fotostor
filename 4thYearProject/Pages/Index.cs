namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class Index : ComponentBase
    {
        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public IShoppingCartService _shoppingCartService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ClaimsPrincipal identity = await _userService.GetUserAsync();

            if (identity.Identity.IsAuthenticated.Equals(true))
            {

                //First get user id
                var ID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault();


                try
                {



                    Console.WriteLine("Can you hear me, Major Tom?");
                    UserData newUser = new UserData();


                    //First get user id
                    var DisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                          .Select(c => c.Value).SingleOrDefault();

                    newUser.Id = ID.ToString();
                    newUser.DisplayName = DisplayName.ToString();

                    await UserDataService.AddUserData(newUser);


                  //  ShoppingCart cart = await _shoppingCartService.GetCart(ID);

                        await _shoppingCartService.AddCart(ID);
                    



                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
