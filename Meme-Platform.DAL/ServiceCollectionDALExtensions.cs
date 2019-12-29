using Meme_Platform.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.DAL
{
    public static class ServiceCollectionDALExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<PlatformContext>(
                options => options.UseNpgsql("Host=127.0.0.1;Database=meme-platform;Username=user;Password=user"));

            services.AddRepo<Comment>();
            services.AddRepo<Content>();
            services.AddRepo<Post>();
            services.AddRepo<PostOfTheDay>();
            services.AddRepo<Profile>();
            services.AddRepo<Vote>();
        }

        private static void AddRepo<TEntity>(this IServiceCollection services)
            where TEntity : class
        {
            services.AddTransient<IRepository<TEntity>, Repository<TEntity>>();
        }
    }
}
