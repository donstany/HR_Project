using IOWebFramework.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IOWebFramework.Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using IOWebFramework.Shared.Common.Contracts;
using IOWebFramework.Shared.Common.Tasks;
using IOWebFrameworkApplicationService.Contracts;
using IOWebFrameworkApplicationService.Services;
using IOWebFramework.Core.Services;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Configuration;
using AutoMapper;
using IOWebFramework.Shared.Common;

namespace IOWebApplicationService.Services
{
    public static class IOServiceAppServiceCollectionExtension
    {
        /// <summary>
        /// Регистрира услугите на приложението в IoC контейнера
        /// </summary>
        /// <param name="services">Регистрирани услуги</param>
        /// <param name="Configuration">Настройки на приложението</param>
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();
            services.AddHostedService<TimedHostedService>();
            //services.AddHostedService<ScheduledHostedService>();
            services.AddScoped<IConsoleTaskExecuteMessageService, ConsoleTaskExecuteMessageService>();
            services.AddScoped<IConsoleTaskRecieverService, ConsoleTaskRecieverService>();
            //services.AddScoped<ISyncService, SyncService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IExperienceCalculator, ExperienceCalculator>();
            services.AddAutoMapper(typeof(IOWebFrameworkProfile).Assembly);
        }

        /// <summary>
        /// Регистрира контекстите на приложението в IoC контейнера
        /// </summary>
        /// <param name="services">Регистрирани услуги</param>
        /// <param name="Configuration">Настройки на приложението</param>
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly("IOWebFramework.Infrastructure"))
                .UseSnakeCaseNamingConvention());

            services.AddScoped<IRepository, Repository>();

        }
    }
}