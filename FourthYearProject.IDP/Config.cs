﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace FourthYearProject.IDP
{
    public static class Config
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
                new("FourthYearProjectapi",
                    "4th Year Project API",
                    new[] {"country"}) //useful when using an authorisation policy
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new()
                {
                    ClientId = "FourthYearProject",
                    ClientName = "4th Year Project",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris = {"https://localhost:44366//authentication/login-callback"},
                    PostLogoutRedirectUris = {"https://localhost:44366//authentication/logout-callback"},
                    AllowedScopes = {"openid", "profile", "email", "FourthYearProjectapi"},
                    AllowedCorsOrigins = {"http://localhost:44341"}
                }
            };
    }
}


//remember, I submitted localhost:44366.....to the hosted IDP and it worked.