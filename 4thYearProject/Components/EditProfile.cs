using _4thYearProject.Shared;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Security.Claims;
namespace _4thYearProject.Server.Components
{
    public partial class EditProfile
    {
        [Inject]
        public IUserService UserService { get; set; }

        public ApplicationUser user;

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

        private async System.Threading.Tasks.Task ResetDialog()
        {
            ClaimsPrincipal identity = await UserService.GetUserAsync();
            var displayname = identity.Claims.Where(c => c.Type.Equals("email"))
          .Select(c => c.Value).SingleOrDefault();

            var profilepic = identity.Claims.Where(c => c.Type.Equals("email"))
          .Select(c => c.Value).SingleOrDefault();

            user = new ApplicationUser { DisplayName = displayname, PhotoFile = System.Convert.FromBase64String(profilepic) };

        }


    }
}
