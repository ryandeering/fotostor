// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace FourthYearProject.IDP
{
    public class TestUsers
    {
        public static List<TestUser> Users = new()
        {
            new()
            {
                SubjectId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                Username = "r.yandeering1@gmail.com",
                Password = "password",
            },
            new()
            {
                SubjectId = "7d6e087e-f7b6-4b58-b6ec-3672b7a41810",
                Username = "ryandeering1@gmail.com",
                Password = "password",
            }
        };
    }
}