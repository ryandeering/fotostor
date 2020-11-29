using _4thYearProject.Server.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Server.Pages
{
    public partial class PostEditBase : ComponentBase
    {
        [Inject]
        public IPostDataService PostDataService { get; set; }
    
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Parameter]
        public int PostId { get; set; }

        public Post Post { get; set; } = new Post();

       // protected string CountryId = string.Empty;


        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;

           // int.TryParse(PostId, out var PostId);

            if (PostId == 0) //new employee is being created
            {
                //add some defaults
                Post = new Post { URL = String.Empty, ThumbnailURL = String.Empty, Caption = String.Empty, UploadDate = DateTime.Now, Likes = 0 };
            }
            else
            {
                Post = await PostDataService.GetPostDetails(PostId); //int.parse
            }

        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;

            if (Post.PostId == 0) //new
            {
                var addedPost = await PostDataService.AddPost(Post);
                if (addedPost != null)
                {
                    StatusClass = "alert-success";
                    Message = "New post added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new post. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await PostDataService.AddPost(Post); //FIX LATER HOLY FUCK
                StatusClass = "alert-success";
                Message = "Post updated successfully.";
                Saved = true;
            }
        }

        protected async Task DeletePost()
        {
            await PostDataService.DeletePost(Post.PostId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}
