using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.IdentityModel.Tokens;

namespace Providers
{
    class CustomTokenValidator : ICustomTokenRequestValidator
    {
        

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            context.Result = new TokenRequestValidationResult(new ValidatedTokenRequest() {  });
            return Task.FromResult(0);
        }

       
    }
    public static class RSACryptoServiceProviderExtensions

    {

        public static void FromXmlStringCustom(this RSACryptoServiceProvider rsa, string xmlString)

        {

            RSAParameters parameters = new RSAParameters();



            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);



            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))

            {

                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)

                {

                    switch (node.Name)

                    {

                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;

                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;

                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;

                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;

                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;

                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;

                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;

                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;

                    }

                }

            }
            else

            {

                throw new Exception("Invalid XML RSA key.");

            }



            rsa.ImportParameters(parameters);

        }



        public static string ToXmlStringCustom(this RSACryptoServiceProvider rsa, bool includePrivateParameters)

        {

            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);



            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",

                Convert.ToBase64String(parameters.Modulus),

                Convert.ToBase64String(parameters.Exponent),

                Convert.ToBase64String(parameters.P),

                Convert.ToBase64String(parameters.Q),

                Convert.ToBase64String(parameters.DP),

                Convert.ToBase64String(parameters.DQ),

                Convert.ToBase64String(parameters.InverseQ),

                Convert.ToBase64String(parameters.D));

        }

    }
    public class RsaKeyGenerationResult
    {
        public string PublicKeyOnly { get; set; }
        public string PublicAndPrivateKey { get; set; }
        public static RsaKeyGenerationResult GenerateKeys()
        {
            RSAParameters publicKey;
            RsaKeyGenerationResult result;
            //using (RSA rsa = RSA.Create())
            //{
            //    rsa.KeySize = 2048;
            //    publicKey = rsa.ExportParameters(true);
            //    result = new RsaKeyGenerationResult
            //    {
            //        PublicAndPrivateKey = rsa.ToXmlString(true),
            //        PublicKeyOnly = rsa.ToXmlString(false)
            //    };
            //}
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(2048);
            publicKey = myRSA.ExportParameters(true);
            result = new RsaKeyGenerationResult
            {
                PublicAndPrivateKey = myRSA.ToXmlStringCustom(true),
                PublicKeyOnly = myRSA.ToXmlStringCustom(true)
            };
            return result;
        }
        public static RsaSecurityKey GetRsaSecurityKey()
        {
            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            RsaKeyGenerationResult keyGenerationResult = RsaKeyGenerationResult.GenerateKeys();
            publicAndPrivate.FromXmlStringCustom(keyGenerationResult.PublicAndPrivateKey);
            return new RsaSecurityKey(publicAndPrivate);
        }
    }
    public class MySigningCredentialStore : ISigningCredentialStore
    {
        public Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            var handler= new JwtSecurityTokenHandler();
            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            RsaKeyGenerationResult keyGenerationResult = RsaKeyGenerationResult.GenerateKeys();
            publicAndPrivate.FromXmlStringCustom(keyGenerationResult.PublicAndPrivateKey);

            var s = new SigningCredentials(new RsaSecurityKey(publicAndPrivate), SecurityAlgorithms.RsaSha256);
            return Task.FromResult(s);
        }

        
    }

    public class MyValidationKeyStore : IValidationKeysStore
    {
        public Task<IEnumerable<SecurityKey>> GetValidationKeysAsync()
        {
            RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();
            RsaKeyGenerationResult keyGenerationResult = RsaKeyGenerationResult.GenerateKeys();
            publicAndPrivate.FromXmlStringCustom(keyGenerationResult.PublicAndPrivateKey);

            var keys = new List<SecurityKey>() { new RsaSecurityKey(publicAndPrivate)};
            return Task.FromResult(keys.AsEnumerable());
        }

       
    }
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