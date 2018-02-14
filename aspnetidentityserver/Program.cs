using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using myAppApi.Data;
using aspnetidentityserver.Models;

namespace aspnetidentityserver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host  = BuildWebHost(args);

            // using(var scope = host.Services.CreateScope()){
            //     var services = scope.ServiceProvider;
            //     try
            //     {
            //         var context = services.GetRequiredService<TodoDbContext>();
            //         DbInitializer.initialize(context);
            //     }
            //     catch (System.Exception ex)
            //     {
            //         var logger = services.GetRequiredService<ILogger<Program>>();
            //         logger.LogError(ex, "Error saat init data ke database");
                    
            //     }
            // }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://+:82")
                .Build();
    }
}
