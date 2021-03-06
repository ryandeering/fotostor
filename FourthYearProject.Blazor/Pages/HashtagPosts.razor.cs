using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FourthYearProject.Blazor.Services;
using FourthYearProject.Blazor.Shared;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models;
using Blazored.Modal;
using Blazored.Modal.Services;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FourthYearProject.Blazor.Pages
{
    public partial class HashtagPosts
    {
        private ClaimsPrincipal identity;
        private string LoggedIn;

        [Parameter] public string HashTag { get; set; }

        public UserData User { get; set; }
        public List<Post> Posts { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        //https://github.com/Blazored/Modal/blob/main/samples/BlazorWebAssembly/Pages/PassDataToModal.razor

        [Inject] public IHashTagDataService HashTagDataService { get; set; }

        [Inject] public IPostDataService PostDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IFollowingDataService FollowingService { get; set; }

        [Inject] public ILikeDataService LikeService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] protected IMatToaster Toaster { get; set; }

        [Inject] public IJSRuntime JsRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();
            if (identity.Identity.IsAuthenticated)
            {
                //First get user claims    
                
                LoggedIn = identity.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value).SingleOrDefault().ToString();

                Posts = (List<Post>) await HashTagDataService.GetLatestPostsByHashTag(HashTag);
                User = await UserDataService.GetUserDataDetailsInFull(LoggedIn);


                foreach (var Post in Posts)
                {
                    var like = await VerifyLike(Post);
                    Post.Liked = like;
                }
            }
        }


        private void BuyLicense(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddLicense.PostId), PostId);

            Modal.Show<AddLicense>("PostId", parameters);
        }

        private void BuyShirt(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddShirt.PostId), PostId);

            Modal.Show<AddShirt>("PostId", parameters);
        }

        private void BuyPrint(int PostId)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(AddPrint.PostId), PostId);

            Modal.Show<AddPrint>("PostId", parameters);
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
    }
}