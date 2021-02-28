using _4thYearProject.Server.Services;
using _4thYearProject.Server.Shared;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class Feed
    {
        [Parameter]
        public string DisplayName { get; set; }
        public UserData User { get; set; }
        public List<Post> Posts { get; set; }

        [Inject]
        public IPostDataService PostDataService { get; set; }
        [Inject]
        public IUserService _userService { get; set; }
        [Inject]
        public IFollowingDataService FollowingService { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        //https://github.com/Blazored/Modal/blob/main/samples/BlazorWebAssembly/Pages/PassDataToModal.razor

        ClaimsPrincipal identity;

        protected async override Task OnInitializedAsync()
        {

            identity = await _userService.GetUserAsync();
            //First get user claims    
            string claimDisplayName = identity.Claims.Where(c => c.Type.Equals("preferred_username"))
                  .Select(c => c.Value).SingleOrDefault().ToString();


            User = await UserDataService.GetUserDataDetailsByDisplayName(claimDisplayName);

            Posts = (await PostDataService.GetAllPostsbyFollowing(User.Id)).ToList();




        }



        async void BuyLicense(int PostId)
        {

        }



        async Task ShowModal(int PostId)
        {

            var parameters = new ModalParameters();
            parameters.Add(nameof(AddLicense.PostId), PostId);




            var addLicense = Modal.Show<AddLicense>("PostId", parameters);
            var result = await addLicense.Result;
        }
    }
}