using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetwebapi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;

namespace aspnetwebapi
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
            var connstring = Configuration.GetConnectionString("todoConnection");
            Console.WriteLine(connstring);

            services.AddDbContext<tododbContext>(d =>
            {
                d.UseSqlServer(Configuration.GetConnectionString("todoConnection"));
            });
            //services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            services.AddMvc();
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(opt =>
            {
                opt.Authority = "http://localhost:82";

                opt.RequireHttpsMetadata = false;
                opt.ApiName = "api";
                opt.ApiSecret = "rahasia";

            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Description = "todo asp.net core web api",
                    Title = "todo-aspnetcore-webapi",
                    Version = "v1",
                    TermsOfService = "none",
                    Contact = new Contact { Email = "gadael.sedubun@visionet.co.id", Name = "GS", Url = "http://www.visionet.co.id" }
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
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(d =>
            {
                d.SwaggerEndpoint("/swagger/v1/swagger.json", "todo asp.net core web api");
            });
        }
    }
}
