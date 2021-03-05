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
        [Inject]
        public NavigationManager UriHelper { get; set; }

        ClaimsPrincipal identity;

        string claimDisplayName { get; set; }


        Following follow = new Following();
        List<Following> followers { get; set; }
        List<Following> following { get; set; }

        bool IsFollowing { get; set; }
        int FollowerCount { get; set; }
        int FollowingCount { get; set; }
        


        

        protected async override Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();
            //First get user claims    
            claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                  .Select(c => c.Value).SingleOrDefault().ToString();


            User = await UserDataService.GetUserDataDetailsByDisplayName(DisplayName);

            Posts = (await PostDataService.GetPostsByUserId(User.Id)).ToList();

            followers = await GetFollowers();
            following = await GetFollowing();
            FollowerCount = followers.Count;
            FollowingCount = following.Count;

            await VerifyFollowing();


        }



        protected async Task FollowUser()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();

            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.AddFollowing(follow);
            IsFollowing = true;
            FollowerCount++;

        }

        protected async Task UnFollowUser()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();

            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.RemoveFollowing(LoggedInID, User.Id);
            IsFollowing = false;
            FollowerCount--;
        }


        protected async Task VerifyFollowing()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();
            follow.Follower_ID = LoggedInID;
            Following temp = await FollowingService.verifyFollowing(LoggedInID, User.Id);
            if (temp != null)
            {
                IsFollowing = true;
            }
            else
            {
                IsFollowing = false;
            }
        }


        protected async Task<List<Following>> GetFollowers()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();
            List<Following> followerstemp = await FollowingService.GetFollowers(User.Id);
           

            return followerstemp;

        }

        protected async Task<List<Following>> GetFollowing()
        {
            List<Following> followingtemp = await FollowingService.GetFollowing(User.Id);

            return followingtemp;

        }

        void Navigate(int PostId)
        {
            UriHelper.NavigateTo("/post/"+PostId);
        }

        void ProfileSettings()
        {
            UriHelper.NavigateTo("/profile/settings");
        }







    }
}
