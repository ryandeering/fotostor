using System;
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
using Microsoft.AspNetCore.Components;

namespace _4thYearProject.Server.Pages
{
    public partial class Feed
    {
        //https://github.com/Blazored/Modal/blob/main/samples/BlazorWebAssembly/Pages/PassDataToModal.razor

        private ClaimsPrincipal identity;
        private string LoggedIn;

        [Parameter] public string DisplayName { get; set; }

        public UserData User { get; set; }
        public List<Post> Posts { get; set; }
        public List<Post> SuggestedPosts { get; set; }
        public List<Post> ActualPosts { get; set; }
        public FeedProfileData ProfileData { get; set; }

        [Inject] public IPostDataService PostDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IFollowingDataService FollowingService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public ISuggestionsDataService SuggestionsDataService { get; set; }

        [Inject] public ILikeDataService LikeService { get; set; }


        [CascadingParameter] public IModalService Modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();
            //First get user claims     
            var claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            LoggedIn = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();


            User = await UserDataService.GetUserDataDetailsByDisplayName(claimDisplayName);

            SuggestedPosts = (await SuggestionsDataService.GetSuggestions(User.Id)).ToList();

            ActualPosts = (await PostDataService.GetAllPostsbyFollowing(User.Id)).ToList();

                


            var PostsBeforeShuffle = new List<Post>(ActualPosts.Count +
                                                    SuggestedPosts.Count);

            PostsBeforeShuffle.AddRange(ActualPosts);
            PostsBeforeShuffle.AddRange(SuggestedPosts);



            foreach (var Post in PostsBeforeShuffle)
            {
                var like = await VerifyLike(Post);
                Post.Liked = like;
                Console.WriteLine(like);
            }




            Posts = PostsBeforeShuffle.OrderBy(x => Guid.NewGuid()).Distinct().ToList();


        }


        protected async Task GiveLike(Post post)
        {
            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            var like = new Like(LoggedInID, post.PostId.ToString());
            await LikeService.AddLike(like); //TODO fix this method
            post.Likes++;
            StateHasChanged();
        }

        protected async Task UnLike(Post post)
        {
            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();


            await LikeService.RemoveLike(post.PostId.ToString(), LoggedInID);
            post.Likes--;
            StateHasChanged();
        }

        protected async Task<bool> VerifyLike(Post post)
        {
            return await LikeService.VerifyLike(post.PostId.ToString(), LoggedIn);
        }


        private async Task BuyLicense(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddLicense.PostId), PostId);

            var addLicense = Modal.Show<AddLicense>("PostId", parameters);
            var result = await addLicense.Result;
        }

        private async Task BuyShirt(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddShirt.PostId), PostId);

            var addShirt = Modal.Show<AddShirt>("PostId", parameters);
            var result = await addShirt.Result;
        }

        private async Task BuyPrint(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddPrint.PostId), PostId);

            var addPrint = Modal.Show<AddPrint>("PostId", parameters);
            var result = await addPrint.Result;
        }

        private async Task<FeedProfileData> GetUserName(string UserId)
        {
            var response = await UserDataService.GetUserNameFromId(UserId);
            return response;
        }
    }
}