// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace FourthYearProject.IDP
{
    public class TestUsers
    {
        public static List<TestUser> Users = new()
        {
            new()
            {
                SubjectId = "06c71238-0137-4df6-bb6a-e50e62a4a7c5",
                Username = "ryandeering1@gmail.com",
                Password = "Mywifedied1!",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Ryan Deering"),
                    new Claim(JwtClaimTypes.GivenName, "Ryan"),
                    new Claim(JwtClaimTypes.FamilyName, "Deering"),
                    new Claim(JwtClaimTypes.Email, "ryandeering1@gmail.com"),
                    new Claim("country", "BE")
                }
            },
            new()
            {
                SubjectId = "37d0f2fa-1069-489f-9d65-48c9ba44639b", Username = "Wendy", Password = "password",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Wendy Torrance"),
                    new Claim(JwtClaimTypes.GivenName, "Wendy"),
                    new Claim(JwtClaimTypes.FamilyName, "Torrance"),
                    new Claim(JwtClaimTypes.Email, "wendy.torrance@email.com"),
                    new Claim("country", "NL")
                }
            }
        };
    }
}