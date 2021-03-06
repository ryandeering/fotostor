using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FourthYearProject.Blazor.Services;
using FourthYearProject.Shared;
using FourthYearProject.Shared.Models;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace FourthYearProject.Blazor.Pages
{
    public partial class ProfileSettings : ComponentBase
    {
        private ClaimsPrincipal identity;

        internal List<string> list = new();

        protected string Message = string.Empty;

        private bool Saved;

        protected string StatusClass = string.Empty;

#pragma warning disable S1104 // Fields should not have public accessibility
        public UserData User = new();
#pragma warning restore S1104 // Fields should not have public accessibility

        [Inject] public IUserService _userService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] protected IMatToaster Toaster { get; set; }

        [Inject] private NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            identity = await _userService.GetUserAsync();
            if (identity.Identity.IsAuthenticated)
            {
                //First get user claims    
                var UserID = identity.Claims.Where(c => c.Type.Equals("sub"))
                    .Select(c => c.Value).SingleOrDefault().ToString();

                User = await UserDataService.GetUserDataDetailsInFull(UserID);

                User.Address ??= new Address(); //generates address for form 
            }
        }

        internal async Task HandleMatFileSelected(IMatFileUploadEntry[] files)
        {
            var file = files.FirstOrDefault();


            if (file == null) return;

            using (var stream = new MemoryStream())
            {
                await file.WriteToStreamAsync(stream);
                User.ProfilePic = Convert.ToBase64String(stream.ToArray());
            }
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            await UserDataService.UpdateUserData(User);
                Toaster.Add("Profile data successfully updated.", MatToastType.Success, "SUCCESS");
            navigationManager.NavigateTo("/profile/"+User.DisplayName, forceLoad:true);

        }
    }
}