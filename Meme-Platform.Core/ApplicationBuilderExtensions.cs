using Meme_Platform.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
                if (context.Database.EnsureCreated())
                {
                    // ST: creates the uuid_generate_v4() extension in postgre.
                    // Tested and working on PostgreSQL 12.
                    context.Database.ExecuteSqlRaw("CREATE EXTENSION IF NOT EXISTS \"uuid-ossp\";");
                }
            }
        }
    }
}
