using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace IOWebFramework
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                //.AddEnvironmentVariables()
                .AddJsonFile("hosting.json", optional: true)
                .AddCommandLine(args)
                .Build();

            var host = Host.CreateDefaultBuilder(args)
                            .ConfigureWebHostDefaults(webBuilder =>
                            {
                                webBuilder.UseConfiguration(config);
                                webBuilder.UseStartup<Startup>();
                            });

            return host.ConfigureAppConfiguration((hostContext, configApp) =>
                         {
                             configApp.AddJsonFile($"appsettings.json", true);
                             configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                         });
        }

    }
}
