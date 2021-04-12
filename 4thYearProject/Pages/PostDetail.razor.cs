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
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace _4thYearProject.Server.Pages
{
    public partial class PostDetail : ComponentBase
    {
        private readonly Following follow = new();
        public Comment extendComment = new();


        private ClaimsPrincipal identity;

        [Parameter] public string PostID { get; set; }

        public UserData User { get; set; }
        public Post post { get; set; }
        public List<Comment> Comments { get; set; }


        [Inject] public IPostDataService PostDataService { get; set; }

        [Inject] public ICommentDataService CommentDataService { get; set; }

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IFollowingDataService FollowingService { get; set; }

        [Inject] public ILikeDataService LikeService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public IMatToaster matToaster { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }

        private string claimDisplayName { get; set; }

        private string LoggedInID { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Comments = (await CommentDataService.GetCommentsByPostId(int.Parse(PostID))).ToList();


                identity = await _userService.GetUserAsync();

                var LoggedIn = identity.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value).SingleOrDefault().ToString();

                User = await UserDataService.GetUserDataDetails(LoggedIn);


                claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                    .Select(c => c.Value).SingleOrDefault().ToString();


                LoggedInID = LoggedIn;


                post = await PostDataService.GetPostDetails(int.Parse(PostID));
                post.Liked = await VerifyLiked();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected async Task FollowUser()
        {
            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.AddFollowing(follow);
            await OnInitializedAsync();
        }


        protected async Task<bool> VerifyLiked()
        {
            return await LikeService.VerifyLike(post.PostId.ToString(), LoggedInID);
        }

        protected async Task GiveLike()
        {
            var like = new Like(LoggedInID, post.PostId.ToString());
            await LikeService.AddLike(like);
            post.Liked = true;
            post.Likes++;
            StateHasChanged();
        }

        protected async Task UnLike()
        {
            await LikeService.RemoveLike(post.PostId.ToString(), LoggedInID);
            post.Liked = false;
            post.Likes--;
            StateHasChanged();
        }

        protected async Task MakeComment()
        {
            extendComment.UserId = LoggedInID;
            extendComment.PostId = post.PostId;
            extendComment.SubmittedOn = DateTime.Now;
            await CommentDataService.AddComment(extendComment);
            await OnInitializedAsync();
            matToaster.Add("Comment successfully made.", MatToastType.Success, "Success");
        }


        protected async Task DeleteComment(Comment comment)
        {
            await CommentDataService.DeleteComment(comment.Id);
            await OnInitializedAsync();
            matToaster.Add("Comment deleted successfully.", MatToastType.Success, "SUCCESS");
        }


        protected async Task UnFollowUser()
        {
            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;
            await FollowingService.RemoveFollowing(LoggedInID, User.Id);
        }


        protected async Task<Following> VerifyFollowing() //TODO do verify following
        {
            follow.Follower_ID = LoggedInID;
            follow.Followed_ID = User.Id;


            return null;
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
    }
}