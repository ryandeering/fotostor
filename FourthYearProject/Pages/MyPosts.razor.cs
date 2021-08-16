using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _4thYearProject.Server.Services;
using _4thYearProject.Server.Shared;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Blazored.Modal;
using Blazored.Modal.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace _4thYearProject.Server.Pages
{
    public partial class MyPosts : ComponentBase
    {
        private readonly Following follow = new();


        private ClaimsPrincipal identity;

        [Parameter] public string DisplayName { get; set; }

        public UserData User { get; set; }
        public List<Post> Posts { get; set; }

        [Inject] public IPostDataService PostDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IFollowingDataService FollowingService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public NavigationManager UriHelper { get; set; }

        [Inject] public IMatToaster Toaster { get; set; }

        private string claimDisplayName { get; set; }

        private string LoggedInID { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        private List<Following> followers { get; set; }
        private List<Following> following { get; set; }

        private bool IsFollowing { get; set; }
        private int FollowerCount { get; set; }
        private int FollowingCount { get; set; }


        protected override async Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();
            if (identity.Identity.IsAuthenticated)
            {
                //First get user claims    
                claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                    .Select(c => c.Value).SingleOrDefault().ToString();

                LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value).SingleOrDefault().ToString();


                User = await UserDataService.GetUserDataDetailsByDisplayName(DisplayName);

                Posts = (await PostDataService.GetPostsByUserId(User.Id)).ToList();

                followers = await GetFollowers();
                following = await GetFollowing();
                FollowerCount = followers.Count;
                FollowingCount = following.Count;

                await VerifyFollowing();
            }
        }


        protected async Task FollowUser()
        {
            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.AddFollowing(follow);
            IsFollowing = true;
            FollowerCount++;
            Toaster.Add("User " + User.DisplayName + " followed!", MatToastType.Success);
        }

        protected async Task UnFollowUser()
        {
            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.RemoveFollowing(LoggedInID, User.Id);
            IsFollowing = false;
            FollowerCount--;
            Toaster.Add("User " + User.DisplayName + " unfollowed.", MatToastType.Warning);
        }


        protected async Task VerifyFollowing()
        {
            follow.Follower_ID = LoggedInID;
            var temp = await FollowingService.verifyFollowing(LoggedInID, User.Id);
            if (temp != null)
                IsFollowing = true;
            else
                IsFollowing = false;
        }


        protected async Task<List<Following>> GetFollowers()
        {
            var followerstemp = await FollowingService.GetFollowers(User.Id);


            return followerstemp;
        }

        protected async Task<List<Following>> GetFollowing()
        {
            var followingtemp = await FollowingService.GetFollowing(User.Id);

            return followingtemp;
        }

        private void Navigate(int PostId)
        {
            UriHelper.NavigateTo("/post/" + PostId);
        }

        private void ProfileSettings()
        {
            UriHelper.NavigateTo("/profile/settings");
        }


        private void ShowFollowers()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(Followers.LoggedIn), LoggedInID);
            Modal.Show<Followers>("Followers", parameters);
        }
    }
}