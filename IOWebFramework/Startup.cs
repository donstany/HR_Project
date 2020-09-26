using AutoMapper;
using DataTables.AspNet.AspNetCore;
using IOWebFramework.Core.Configuration;
using IOWebFramework.Core.Constants;
using IOWebFramework.Extensions;
using IOWebFramework.Infrastructure.Data;
using IOWebFramework.Infrastructure.Data.Seed;
using IOWebFramework.ModelBinders;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using System;

namespace IOWebFramework
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
            // За добавяне на контексти, използвайте extension метода!!!
            services.AddApplicationDbContext(Configuration);

            services.AddCdn<ApplicationDbContext>();

            services.AddAuthentication(x =>
            {
                x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            });

            // За конфигуриране на Identity, използвайте extension метода!!! 
            services.AddApplicationIdentity();

            // За добавяне на услуги, използвайте extension метода!!!
            services.AddApplicationServices();

            //Добавя филтър, който не изисква да се пишат атрибути в контролирите
            //services.AddControllersWithViews(options => options
            //         .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time   
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider(FormatConstant.NormalDateFormat));
                    options.ModelBinderProviders.Insert(1, new DoubleModelBinderProvider());
                    options.ModelBinderProviders.Insert(2, new DecimalModelBinderProvider());
                });

            //services.AddRazorPages();
            services.RegisterDataTables();
            services.AddAutoMapper(typeof(IOWebFrameworkProfile).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var isDevelopmentEnvironment = env.IsDevelopment();
            var isQaEnvironment = Configuration["ASPNETCORE_ENVIRONMENT"] == "QA" ? true : false;
            if (isDevelopmentEnvironment || isQaEnvironment)
            {
                app.UseDeveloperExceptionPage();
                app.EnsureDatabaseIsSeeded(true);
                app.UseDatabaseErrorPage();
            }
            else //Production ENVIRONMENT
            {
                app.EnsureDatabaseIsSeeded(true);
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env);
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
