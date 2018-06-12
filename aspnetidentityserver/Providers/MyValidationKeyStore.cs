using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.IdentityModel.Tokens;

namespace aspnetidentityserver.Providers
{
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
}