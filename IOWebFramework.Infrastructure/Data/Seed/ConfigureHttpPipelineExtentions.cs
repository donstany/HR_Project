using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IOWebFramework.Infrastructure.Data.Seed

{
    /// <summary>
    /// This class is based on some of the suggestions bty K. Scott Allen in
    /// his NDC 2017 talk https://www.youtube.com/watch?v=6Fi5dRVxOvc
    /// </summary>
    public static class ConfigureHttpPipelineExtentions
    {
        public static void EnsureDatabaseIsSeeded(this IApplicationBuilder applicationBuilder, bool isDevelopmentEnvironment = false)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.EnsureSeedData(isDevelopmentEnvironment);
        }
    }
}