using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourthYearProject.Blazor.Services;
using FourthYearProject.Shared.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FourthYearProject.Blazor.Pages
{
    public class PostEditBase : ComponentBase
    {
#pragma warning disable S1104 // Fields should not have public accessibility
        public bool fileUploaded;
#pragma warning restore S1104 // Fields should not have public accessibility

        protected string Message = string.Empty;

        protected bool Saved;

        internal IReadOnlyList<IBrowserFile> selectedFiles;

        protected string StatusClass = string.Empty;

#pragma warning disable S1104 // Fields should not have public accessibility
        public bool value;
#pragma warning restore S1104 // Fields should not have public accessibility
        [Inject] public IPostDataService PostDataService { get; set; }

        [Inject] public IHashTagDataService HashTagDataService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        [Inject] public IMatToaster Toaster { get; set; }

        [Parameter] public int PostId { get; set; }

        public Post Post { get; set; } = new();

        [Parameter] public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public double Price { get; set; }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            Price = 0.00;
        }

        protected async Task HandleValidSubmitPrice()
        {
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            if (PostId == 0)
            {
                fileUploaded = false;
                //add some defaults
                Post = new Post
                {
                    Caption = string.Empty, PostDeleted = false, PhotoFile = null, Comments = new List<Comment>(),
                    UploadDate = DateTime.Now, Likes = 0
                };
            }
            else
            {
                fileUploaded = true;
                Post = await PostDataService.GetPostDetails(PostId); //int.parse
            }
        }

        protected void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;


            if (Post.PostId == 0) //new
            {
                var addedPost = await PostDataService.AddPost(Post);
                if (addedPost != null)
                {
                    PostId = addedPost.PostId;
                    Toaster.Add("Post added successfully.", MatToastType.Success, "SUCCESS");
                    Saved = true;
                    NavigationManager.NavigateTo("/feed/");
                }
                else
                {
                    Saved = false;
                    NavigationManager.NavigateTo("/postedit/");
                }
            }
            else
            {
                await PostDataService.AddPost(Post);
            }
        }

        protected async Task DeletePost()
        {
            await PostDataService.DeletePost(Post.PostId);
            Toaster.Add("Post deleted.", MatToastType.Danger);

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/profile/");
        }

        public void OnChange(bool? value, string name) //I promise this makes sense
        {
            if (value == true) ShowDialog = true;
        }
    }
}