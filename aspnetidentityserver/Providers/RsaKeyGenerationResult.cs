using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace myAppApi.Providers
{
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
}