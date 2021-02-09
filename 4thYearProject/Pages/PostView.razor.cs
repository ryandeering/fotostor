using _4thYearProject.Server.Services;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class PostView : ComponentBase
    {
        [Parameter]
        public string PostID { get; set; }

        public UserData User { get; set; }
        public Post post { get; set; }
        public List<Comment> Comments { get; set; }
        Following follow = new Following();
        Comment extendcomment = new Comment();


        [Inject]
        public IPostDataService PostDataService { get; set; }
        [Inject]
        public ICommentDataService CommentDataService { get; set; }
        [Inject]
        public IUserService _userService { get; set; }
        [Inject]
        public IFollowingDataService FollowingService { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IMatToaster matToaster { get; set; }



        ClaimsPrincipal identity;

        string claimDisplayName { get; set; }





        protected async Task MakeComment()
        {

            try
            {
                string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault().ToString();
                extendcomment.UserId = LoggedInID;
                extendcomment.PostId = post.PostId;
                extendcomment.SubmittedOn = DateTime.Now;
                await CommentDataService.AddComment(extendcomment);
                await OnInitializedAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger);
            }


        }



        protected async Task DeleteComment(int CommentID)
        {

            try
            {
                await CommentDataService.DeleteComment(CommentID);
                await OnInitializedAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // matToaster.Add(ex.GetBaseException().Message, MatToastType.Danger);
            }


        }







        protected async override Task OnInitializedAsync()
        {

            identity = await _userService.GetUserAsync();

            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                 .Select(c => c.Value).SingleOrDefault().ToString();

            User = await UserDataService.GetUserDataDetails(LoggedInID);



            claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
               .Select(c => c.Value).SingleOrDefault().ToString();

            post = (await PostDataService.GetPostDetails(int.Parse(PostID)));
            Comments = (await CommentDataService.GetCommentsByPostId(int.Parse(PostID))).ToList();


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
