﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using aspnetidentityserver.Models;
using aspnetidentityserver.Providers;

namespace aspnetidentityserver
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddAuthorization();
            services.AddMvc();
            
            var signingKeys = new MySigningCredentialStore().GetSigningCredentialsAsync().Result;
            var validationKeys = RsaKeyGenerationResult.GetRsaSecurityKey();
            var authOpt = new AuthenticationOptions { };
            
            services.AddIdentityServer()
                .AddSigningCredential(signingKeys)
                .AddValidationKeys(new AsymmetricSecurityKey[] { validationKeys })
                .AddResourceOwnerValidator<MyUserPasswordValidator>()
                .AddClientStore<MyClient>()
                .AddResourceStore<MyResourceStore>()
                .AddJwtBearerClientAuthentication();

            services.AddDbContext<TodoDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("todoConnection"));
            });
            services.AddCors(cors=>{
                cors.AddPolicy("localcors", d=>{
                    d.AllowAnyOrigin();
                    d.AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            //app.UseCors("localCors");


            app.UseMvcWithDefaultRoute();

            
        }
    }
}
