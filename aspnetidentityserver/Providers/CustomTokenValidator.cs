using System.Threading.Tasks;
using IdentityServer4.Validation;

namespace aspnetidentityserver.Providers
{
    public class CustomTokenValidator : ICustomTokenRequestValidator
    {
        

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            context.Result = new TokenRequestValidationResult(new ValidatedTokenRequest() {  });
            return Task.FromResult(0);
        }

       
    }
}