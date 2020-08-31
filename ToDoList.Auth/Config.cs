﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Google.Apis.Auth.OAuth2;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ToDoList.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("todolist-api")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "todolist-app",
                    ClientSecrets =
                    {
                        new Secret("thisismyclientspecificsecret".Sha256())
                    },
                    AllowedScopes = { "todolist-api" }
                }
            };

    }
}