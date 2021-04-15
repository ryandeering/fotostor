// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace FourthYearProject.IDP
{
    public static class ConfigProd
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new("country", new[] {"country"})
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new("_4thyearprojectapi",
                    "4th Year Project API",
                    new[] {"country"}) //useful when using an authorisation policy
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new()
                {
                    ClientId = "_4thyearproject",
                    ClientName = "4th Year Project",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = {"https://fourthyrprojblazor.azurewebsites.net/authentication/login-callback"},
                    PostLogoutRedirectUris =
                        {"https://fourthyrprojblazor.azurewebsites.net/authentication/logout-callback"},
                    AllowedScopes = {"openid", "profile", "email", "_4thyearprojectapi"},
                    AllowedCorsOrigins =
                    {
                        "http://fourthyrprojblazor.azurewebsites.net", "https://fourthyrprojblazor.azurewebsites.net",
                        "https://fotostopapi.azurewebsites.net/"
                    }
                }
            };
    }
}