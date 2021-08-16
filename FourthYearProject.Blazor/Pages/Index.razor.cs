using System;
using System.Linq;
using System.Threading.Tasks;
using FourthYearProject.Blazor.Services;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace FourthYearProject.Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IShoppingCartService _shoppingCartService { get; set; }

        [Inject] public NavigationManager Nav { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var identity = await _userService.GetUserAsync();

                if (identity.Identity.IsAuthenticated)
                {
                    //First get user id
                    var ID = identity.Claims.Where(c => c.Type.Equals("sub"))
                        .Select(c => c.Value).SingleOrDefault();


                    try
                    {
                        var newUser = new UserData();


                        //First get user id
                        var DisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                            .Select(c => c.Value).SingleOrDefault();


                        var Email = identity.Claims.Where(c => c.Type.Equals("email"))
                            .Select(c => c.Value).SingleOrDefault();


                        newUser.Id = ID;
                        newUser.DisplayName = DisplayName;
                        newUser.Email = Email;
                        newUser.Bio = string.Empty;

                        await UserDataService.AddUserData(newUser);
                        await _shoppingCartService.AddCart(ID);


                        Nav.NavigateTo("/feed/", true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}