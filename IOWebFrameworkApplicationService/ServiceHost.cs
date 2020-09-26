using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using IOWebApplicationService.Services;
using AutoMapper;
using IOWebFramework.Core.Configuration;

namespace IOWebFrameworkApplicationService
{
    public class ServiceHost
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = new HostBuilder()
                       .ConfigureHostConfiguration(configHost =>
                       {
                           configHost.SetBasePath(Directory.GetCurrentDirectory());
                           configHost.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                           configHost.AddCommandLine(args);
                       })
                       .ConfigureAppConfiguration((hostContext, configApp) =>
                       {
                           configApp.SetBasePath(Directory.GetCurrentDirectory());
                           configApp.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                           configApp.AddJsonFile($"appsettings.json", true);
                           configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                           configApp.AddCommandLine(args);
                       })
                      .ConfigureServices((hostContext, services) =>
                      {
                          services.AddAppDbContext(hostContext.Configuration);
                          services.ConfigureServices(hostContext.Configuration);
                          services.AddAutoMapper(typeof(IOWebFrameworkProfile).Assembly);
                      })
                      .ConfigureLogging((hostContext, configLogging) =>
                      {
                          configLogging.AddConsole();
                          configLogging.AddDebug();
                      });
            return host;
        }
    }
}
