using _4thYearProject.Server.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;


namespace _4thYearProject.Server.Components
{
    public partial class AddPostDialog
    {
        public Post Post { get; set; } =
            new Post { PostId = 1, URL = String.Empty, ThumbnailURL = String.Empty, Caption = String.Empty, UploadDate = DateTime.Now, Likes = 0 };

        [Inject]
        public IPostDataService PostDataService { get; set; }

        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

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
            Post = new Post { PostId = 1, URL = String.Empty, ThumbnailURL = String.Empty, Caption = String.Empty, UploadDate = DateTime.Now, Likes = 0 };
        }

        protected async Task HandleValidSubmit()
        {
            await PostDataService.AddPost(Post);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
