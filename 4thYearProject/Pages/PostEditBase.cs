namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared.Models;
    using MatBlazor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial class PostEditBase : ComponentBase
    {
        [Inject]
        public IPostDataService PostDataService { get; set; }

        [Inject]
        public IHashTagDataService HashTagDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMatToaster Toaster { get; set; }

        [Parameter]
        public int PostId { get; set; }

        public Post Post { get; set; } = new Post();

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public double Price { get; set; }

        public bool value;

        public bool fileUploaded = false;

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

        protected string Message = string.Empty;

        protected string StatusClass = string.Empty;

        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            if (PostId == 0) 
            {
                //add some defaults
                Post = new Post { Caption = String.Empty, PostDeleted = false, PhotoFile = null, Comments = new List<Comment>(), UploadDate = DateTime.Now, Likes = 0 };
            }
            else
            {
                fileUploaded = true;
                Post = await PostDataService.GetPostDetails(PostId); //int.parse
            }
        }

        internal IReadOnlyList<IBrowserFile> selectedFiles;

        protected void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            this.StateHasChanged();
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

                }
                else
                {
                    Saved = false;
                }
            }
            else
            {
                await PostDataService.AddPost(Post); //TODO FIX LATER HOLY FUCK
                StatusClass = "alert-success";
                Message = "Fuck.";
                Saved = true;
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

            if (value == true)
            {
                ShowDialog = true;
            }
        }
    }
}
