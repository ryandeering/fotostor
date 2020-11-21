using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4thYearProject.Server.MessageHandlers
{
   public class FourthYearProjectAPIAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public FourthYearProjectAPIAuthorizationMessageHandler(
        IAccessTokenProvider provider, NavigationManager navigation)
        : base(provider, navigation)
    {
        ConfigureHandler(
              authorizedUrls: new[] { "https://localhost:44340/" });
    }
}
}
