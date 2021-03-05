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
    public partial class PostDetail : ComponentBase
    {
        [Parameter]
        public string PostID { get; set; }

        public UserData User { get; set; }
        public Post post { get; set; }
        public List<Comment> Comments { get; set; }
        Following follow = new Following();
        Like like = new Like();
        bool liked = false;
        int LikeCount { get; set; }
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
        public ILikeDataService LikeService { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IMatToaster matToaster { get; set; }


        ClaimsPrincipal identity;

        string claimDisplayName { get; set; }

        public UsernameList Usernames = new UsernameList();
        List<string> list = new List<string>();

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


        string getUsername(int pos)
        {
            return list.ElementAt(pos);
        }



        protected async Task GetUsernames()
        {

            try
            {
                UsernameList UsernameIds = new UsernameList();
                Usernames = new UsernameList();


                foreach (var Comment in Comments)
                {
                    UsernameIds.ListofUsernames.Add(Comment.UserId);
                    Console.WriteLine(Comment.UserId);
                }

                Usernames = await UserDataService.GetUserNameFromId(UsernameIds);
                for (int i = 0; i < Comments.Count; i++)
                {
                    Comments[i].Username = Usernames.ListofUsernames[i];
                    Console.WriteLine(Comments[i].Username);
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }


        }





        protected async override Task OnInitializedAsync()
        {

            try
            {
                Comments = (await CommentDataService.GetCommentsByPostId(int.Parse(PostID))).ToList();

                // await GetUsernames();

                identity = await _userService.GetUserAsync();

                string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                     .Select(c => c.Value).SingleOrDefault().ToString();

                User = await UserDataService.GetUserDataDetails(LoggedInID);



                claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                   .Select(c => c.Value).SingleOrDefault().ToString();

                post = (await PostDataService.GetPostDetails(int.Parse(PostID)));
                await VerifyLiked();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }



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



        protected async Task VerifyLiked()
        {
            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();
            Like temp = await LikeService.VerifyLike(LoggedInID, post.PostId.ToString());
            if (temp != null)
            {
                liked = true;
            }
            else
            {
                liked = false;
            }
        }

        protected async Task GiveLike()
        {

            if (!liked)
            {

                string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault().ToString();

                like.User_ID = LoggedInID;
                like.Post_ID = post.PostId.ToString();
                await LikeService.AddLike(like);
                liked = true;
                await OnInitializedAsync();
            }
        }

        protected async Task UnLike()
        {

            if (!liked)
            {

                string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault().ToString();

                like.User_ID = LoggedInID;
                like.Post_ID = post.PostId.ToString();
                await LikeService.RemoveLike(like.Post_ID,like.User_ID);
                liked = false;
                await OnInitializedAsync();
            }
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
