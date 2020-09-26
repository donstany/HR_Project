using IO.LogOperation.Service;
using IOWebFramework.Core.Contracts;
using IOWebFramework.Core.Services;
using IOWebFramework.Core.Services.AttachedDocuments;
using IOWebFramework.Core.Services.File;
using IOWebFramework.Infrastructure.Contracts;
using IOWebFramework.Infrastructure.Data;
using IOWebFramework.Infrastructure.Data.Common;
using IOWebFramework.Infrastructure.Data.Models.Identity;
using IOWebFramework.Infrastructure.Data.Models.UserContext;
using IOWebFramework.Shared.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IOWebFramework.Extensions
{
    /// <summary>
    /// Описва услугите и контекстите на приложението
    /// </summary>
    public static class IOWebAppServiceCollectionExtension
    {
        /// <summary>
        /// Регистрира услугите на приложението в  IoC контейнера
        /// </summary>
        /// <param name="services">Регистрирани услуги</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x => {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            services.AddScoped<INomenclatureService, NomenclatureService>();
            services.AddScoped<IExperienceCalculator, ExperienceCalculator>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDossierService, DossierService>();
            services.AddScoped<IClassifierService, ClassifierService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ILogOperationService<ApplicationDbContext>, LogOperationService<ApplicationDbContext>>();
            services.AddScoped<ICertificateNameIssuerService, CertificateNameIssuerService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectDetailService, ProjectDetailService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAttachedDocumentService, AttachedDocumentService>();
            services.AddScoped<CDN.Core3.Data.Contracts.ICdnService, CDN.Core3.Data.Services.CdnService>();
            services.AddScoped<IMyDossierService, MyDossierService>();
        }

        /// <summary>
        /// Регистрира контекстите на приложението в IoC контейнера
        /// </summary>
        /// <param name="services">Регистрирани услуги</param>
        /// <param name="Configuration">Настройки на приложението</param>
        public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), 
                    m => m.MigrationsAssembly("IOWebFramework.Infrastructure"))
                .UseSnakeCaseNamingConvention());

            services.AddDbContext<CdnDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("CDNConnection")));

            services.AddScoped<IRepository, Repository>();
        }

        /// <summary>
        /// Регистрира Identity provider в IoC контейнера
        /// </summary>
        /// <param name="services">Регистрирани услуги</param>
        public static void AddApplicationIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedAccount = true;

            })
            .AddUserStore<ApplicationUserStore>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddDefaultTokenProviders();
        }
    }
}
