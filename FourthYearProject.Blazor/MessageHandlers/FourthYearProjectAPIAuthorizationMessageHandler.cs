using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace FourthYearProject.Blazor.MessageHandlers
{
    public class FourthYearProjectApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public FourthYearProjectApiAuthorizationMessageHandler(
            IAccessTokenProvider provider, NavigationManager navigation)
            : base(provider, navigation)
        {
            ConfigureHandler(
                new[]
                {
                    "https://fotostopapi.azurewebsites.net", "https://localhost:44340/",
                    "https://4thyearprojectrd.azurewebsites.net", "https://fotostopidp.azurewebsites.net",
                    "https://red-pebble-0ad568c03.azurestaticapps.net"
                });
        }
    }
}