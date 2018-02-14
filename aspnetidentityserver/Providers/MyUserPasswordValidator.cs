using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using aspnetidentityserver.Models;

namespace aspnetidentityserver.Providers
{
    public class MyUserPasswordValidator : IdentityServer4.Validation.IResourceOwnerPasswordValidator
    {
        private TodoDbContext Db;

        public MyUserPasswordValidator(TodoDbContext Context)
        {
            this.Db = Context;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if(string.IsNullOrEmpty( context.UserName) || string.IsNullOrEmpty( context.Password))
                return Task.FromResult(0);
            
            var user = Db.MyUserRoles.FirstOrDefault(d => d.MyUser.UserName == context.UserName && d.MyUser.Password == context.Password);
            Db.Entry(user).Reference(x => x.MyUser).Load();
            if (user != null)
            {
                var customResponse = new Dictionary<string, object>() { { "email", user.MyUser.Email } };
                var claims = new ClaimsIdentity();
                var roles = (from u in Db.MyUserRoles
                             join r in Db.MyRoles on u.MyRole.ID equals r.ID
                             where u.MyUser == user.MyUser
                             select new { r.Role }
                        ).ToList();
                foreach (var item in roles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, item.Role));
                }
                claims.AddClaim(new Claim(ClaimTypes.Name, user.MyUser.UserName));
                claims.AddClaim(new Claim(ClaimTypes.Email, user.MyUser.Email));
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.MyUser.UserName));
                //claims.AddClaim(new Claim("sub", user.MyUser.UserName));
                var principal = new ClaimsPrincipal();
                principal.AddIdentity(claims);
                
                //context.Result = new GrantValidationResult(user.MyUser.Email,CookieAuthenticationDefaults.AuthenticationScheme, claims.Claims, "local", customResponse);
                context.Result = new GrantValidationResult(user.MyUser.Email,CookieAuthenticationDefaults.AuthenticationScheme, claims.Claims, "local", customResponse);
                //context.Result = new GrantValidationResult(principal,customResponse);

            }
            return Task.FromResult(0);
        }
    }
}