using Meme_Platform.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static void SetupDB(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PlatformContext>();
                // TODO: Use migrations you filthy peasant.
                context.Database.EnsureCreated();
            }
        }
    }
}
