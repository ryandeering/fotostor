using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace _4thYearProject.Server.MessageHandlers
{
    public class FourthYearProjectApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public FourthYearProjectApiAuthorizationMessageHandler(
            IAccessTokenProvider provider, NavigationManager navigation)
            : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:44340/", "https://4thyearprojectrd.azurewebsites.net", "https://fotostopidp.azurewebsites.net", "https://gentle-stone-043437c03.azurestaticapps.net" });
        }
    }
}
