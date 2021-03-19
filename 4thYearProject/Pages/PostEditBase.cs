namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared.Models;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public partial class PostEditBase : ComponentBase
    {
        [Inject]
        public IPostDataService PostDataService { get; set; }

        [Inject]
        public IHashTagDataService HashTagDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int PostId { get; set; }

        public Post Post { get; set; } = new Post();

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public double Price { get; set; }

        public bool value;

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

            if (PostId == 0) //new employee is being created
            {
                //add some defaults
                Post = new Post { Caption = String.Empty, PhotoFile = null, Comments = new List<Comment>(), UploadDate = DateTime.Now, Likes = 0 };
            }
            else
            {
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
                Message = "Fuck.";
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

        public void OnChange(bool? value, string name)
        {

            if (value == true)
            {
                ShowDialog = true;
            }
        }
    }
}
