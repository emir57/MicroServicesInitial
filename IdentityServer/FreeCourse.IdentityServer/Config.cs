// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource(){Name="roles",DisplayName="Roles",Description="Users Roles",UserClaims=new[]{"role"}}
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Full Access for catalog api"),
                new ApiScope("photo_stock_fullpermission","Full Access for photo stock api"),
                new ApiScope("basket_fullpermission","Full Access for basket api"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission" } },
                new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
                new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}}
    };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "WebClient",
                    ClientName="Web Client",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { 
                        "catalog_fullpermission", 
                        "photo_stock_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName}
                },
                new Client
                {
                    ClientId = "WebClient2",
                    ClientName = "Web Client2",
                    RequireClientSecret=false,
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={
                        "catalog_fullpermission",
                        "photo_stock_fullpermission",
                        "basket_fullpermission",
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "roles"},
                    AccessTokenLifetime = 1*60*60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}