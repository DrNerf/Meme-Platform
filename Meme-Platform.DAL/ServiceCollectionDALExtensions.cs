using Meme_Platform.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.DAL
{
    public static class ServiceCollectionDALExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
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
