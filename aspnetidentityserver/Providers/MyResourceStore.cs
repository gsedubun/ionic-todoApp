﻿using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.IdentityModel.Tokens;

namespace aspnetidentityserver.Providers
{
    internal class MyResourceStore : IResourceStore
    {
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiresources = GetApiResource().FirstOrDefault(d=> d.Name==name);
            return Task.FromResult(apiresources);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apiresources = GetApiResource();
            return Task.FromResult(apiresources);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var identity = new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

            return Task.FromResult(identity.AsEnumerable());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(GetResources());
        }

        private IEnumerable<ApiResource> GetApiResource()
        {
            var apiresources = new List<ApiResource>(){ new ApiResource()
            {
                Name = "api",
                DisplayName = "web api",
                ApiSecrets = new List<Secret>() { new Secret("rahasia".Sha256()) },
                UserClaims = new List<string>() { ClaimTypes.Name, ClaimTypes.Email, ClaimTypes.NameIdentifier },
                Scopes = new List<Scope>() { new Scope("webapi1"), new Scope("webapi2")}
               

            } };
            return apiresources;
        }

        private Resources GetResources()
        {
            var identity = new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
            var res = new Resources(identity, GetApiResource());
            return res;
        }
    }
}