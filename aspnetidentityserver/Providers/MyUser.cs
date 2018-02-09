using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Providers
{
    internal class MyClient : IClientStore
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = new Client()
            {
                ClientId = clientId,
                ClientSecrets = new List<Secret> { new Secret("rahasia".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = new List<string> { "openid", "webapi1" },
                //AllowedCorsOrigins=new,
                RequireConsent = false,
                

            };
            return Task.FromResult(client);
        }
    }
}