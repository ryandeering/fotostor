﻿namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models;
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

        [Inject]
        public NavigationManager Nav { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {



                ClaimsPrincipal identity = await _userService.GetUserAsync();

                if (identity.Identity.IsAuthenticated.Equals(true))
                {

                    //First get user id
                    var ID = identity.Claims.Where(c => c.Type.Equals("sub"))
                          .Select(c => c.Value).SingleOrDefault();


                    try
                    {

                        UserData newUser = new UserData();


                        //First get user id
                        var DisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                              .Select(c => c.Value).SingleOrDefault();


                        var Email = identity.Claims.Where(c => c.Type.Equals("email"))
                              .Select(c => c.Value).SingleOrDefault();


                        newUser.Id = ID.ToString();
                        newUser.DisplayName = DisplayName.ToString();
                        newUser.Email = Email;
                        newUser.Bio = String.Empty;

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