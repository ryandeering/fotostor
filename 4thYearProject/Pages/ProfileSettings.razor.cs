namespace _4thYearProject.Server.Pages
{
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

    public partial class ProfileSettings : ComponentBase
    {
        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        protected IMatToaster Toaster { get; set; }

        public UserData User = new UserData();

        internal List<string> list = new List<string>();

        private ClaimsPrincipal identity;

        private bool Saved = false;

        protected string Message = string.Empty;

        protected string StatusClass = string.Empty;

        protected override async Task OnInitializedAsync()
        {

            identity = await _userService.GetUserAsync();
            //First get user claims    
            String UserID = identity.Claims.Where(c => c.Type.Equals("sub"))
                  .Select(c => c.Value).SingleOrDefault().ToString();

            User = await UserDataService.GetUserDataDetailsInFull(UserID);

            User.Address ??= new Address();
        }

        internal async Task HandleMatFileSelected(IMatFileUploadEntry[] files)
        {
            IMatFileUploadEntry file = files.FirstOrDefault();


            if (file == null)
            {
                return;
            }

            using (var stream = new System.IO.MemoryStream())
            {
                await file.WriteToStreamAsync(stream);
                User.ProfilePic = Convert.ToBase64String(stream.ToArray());
            }

        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            try
            {
                await UserDataService.UpdateUserData(User);
                Toaster.Add("Profile data successfully updated.", MatToastType.Success, "SUCCESS");
            }
            catch
            {
                Toaster.Add("Something went wrong. Please try again.", MatToastType.Danger, "ERROR");
                Console.WriteLine("ruh roh"); //todo
            }
        }
    }
}
