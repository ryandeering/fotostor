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
using Microsoft.JSInterop;

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

        [Inject] public IPostDataService PostDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IFollowingDataService FollowingService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public ISuggestionsDataService SuggestionsDataService { get; set; }

        [Inject] public ILikeDataService LikeService { get; set; }

        [Inject] protected IMatToaster Toaster { get; set; }

        [Inject] public IJSRuntime JsRuntime { get; set; }


        [CascadingParameter] public IModalService Modal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();
            if (identity.Identity.IsAuthenticated)
            {


                LoggedIn = identity.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value).SingleOrDefault().ToString();



                User = await UserDataService.GetUserDataDetails(LoggedIn);

                SuggestedPosts = (await SuggestionsDataService.GetSuggestions(User.Id)).ToList();

                ActualPosts = (await PostDataService.GetAllPostsbyFollowing(User.Id)).ToList();

                var PostsCombined = new List<Post>(ActualPosts.Count +
                                                   SuggestedPosts.Count);

                PostsCombined.AddRange(ActualPosts);
                PostsCombined.AddRange(SuggestedPosts);


                foreach (var Post in PostsCombined)
                {
                    var like = await VerifyLike(Post);
                    Post.Liked = like;
                }

                Posts = PostsCombined.GroupBy(p => p.PostId).Select(p => p.First()).OrderByDescending(p => p.UploadDate)
                    .ToList();
            }
        }


        protected async Task GiveLike(Post post)
        {
            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            var like = new Like(LoggedInID, post.PostId.ToString());
            await LikeService.AddLike(like);
            post.Liked = true;
            post.Likes++;
            StateHasChanged();
        }

        protected async Task UnLike(Post post)
        {
            var LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();


            await LikeService.RemoveLike(post.PostId.ToString(), LoggedInID);
            post.Liked = false;
            post.Likes--;
            StateHasChanged();
        }

        protected async Task<bool> VerifyLike(Post post)
        {
            return await LikeService.VerifyLike(post.PostId.ToString(), LoggedIn);
        }

        protected async Task DeletePost(Post post)
        {
            if (!await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this post?")) return;
            await PostDataService.DeletePost(post.PostId);
            StateHasChanged();
            Toaster.Add("Post deleted.", MatToastType.Success, "SUCCESS");
            Posts.Remove(post);
        }

        private void BuyLicense(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddLicense.PostId), PostId);

            Modal.Show<AddLicense>("Buy License", parameters);
        }

        private void BuyShirt(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddShirt.PostId), PostId);

            Modal.Show<AddShirt>("Buy Shirt", parameters);
        }

        private void BuyPrint(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddPrint.PostId), PostId);

            Modal.Show<AddPrint>("Buy Print", parameters);
        }
    }
}