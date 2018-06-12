using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.IdentityModel.Tokens;

namespace aspnetidentityserver.Providers
{
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
}