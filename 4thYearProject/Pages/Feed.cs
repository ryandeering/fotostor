using _4thYearProject.Server.Services;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class Feed
{
        [Parameter]
        public string DisplayName { get; set; }
        public UserData User { get; set; }
        public List<Post> Posts { get; set; }

        [Inject]
        public IPostDataService PostDataService { get; set; }
        [Inject]
        public IUserService _userService { get; set; }
        [Inject]
        public IFollowingDataService FollowingService { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        ClaimsPrincipal identity;

        protected async override Task OnInitializedAsync()
        {

            identity = await _userService.GetUserAsync();
            //First get user claims    
            string claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                  .Select(c => c.Value).SingleOrDefault().ToString();


            User = await UserDataService.GetUserDataDetailsByDisplayName(claimDisplayName);

            Posts = (await PostDataService.GetAllPostsbyFollowing(User.Id)).ToList();




        }
    }
}
