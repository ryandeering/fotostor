using _4thYearProject.Server.Services;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class MyPosts : ComponentBase
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

        string claimDisplayName { get; set; }


        Following follow = new Following();

        protected async override Task OnInitializedAsync()
        {

            identity = await _userService.GetUserAsync();
            //First get user claims    
            claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                  .Select(c => c.Value).SingleOrDefault().ToString();


            User = await UserDataService.GetUserDataDetailsByDisplayName(DisplayName);

            Posts = (await PostDataService.GetPostsByUserId(User.Id)).ToList();


        }

        protected async Task FollowUser()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();

            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.AddFollowing(follow);
            await OnInitializedAsync();

        }

        protected async Task UnFollowUser()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();

            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.RemoveFollowing(LoggedInID, User.Id);

        }


        protected async Task<Following> VerifyFollowing()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();
            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            // follow = await FollowingService.verifyFollowing(follow);



            return null;

        }
    }
}
