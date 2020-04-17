using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Classes;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.Core.Transformers.Classes;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.Core
{
    public static class ServiceCollectionCoreExtensions
    {
        public static void Bootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<CoreConfig>();
            services.AddSingleton<UIConfig>();
            services.AddRepositories(configuration);
            services.AddTransformers();
            services.AddServices();
        }

        private static void AddTransformers(this IServiceCollection services)
        {
            services.AddTransformer<Comment, CommentModel, CommentToCommentModelTransformer>();
            services.AddTransformer<Content, ContentModel, ContentToContentModelTransformer>();
            services.AddTransformer<Post, PostModel, PostToPostModelTransformer>();
            services.AddTransformer<Profile, ProfileModel, ProfileToProfileModelTransformer>();
            services.AddTransformer<Vote, VoteModel, VoteToVoteModelTransformer>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddService<IPostService, PostService>();
            services.AddService<IProfileService, ProfileService>();
            services.AddService<IContentService, ContentService>();
            services.AddService<ICommentService, CommentService>();
        }

        private static void AddService<TInterface, TImpl>(this IServiceCollection services)
            where TInterface : class, IServiceBase
            where TImpl : class, TInterface
        {
            services.AddTransient<TInterface, TImpl>();
        }

        private static void AddTransformer<TSource, TDestination, TImpl>(
            this IServiceCollection services)
            where TImpl : class, ITransformer<TSource, TDestination>
        {
            services.AddTransient<ITransformer<TSource, TDestination>, TImpl>();
            services.AddTransient<ICollectionTransformer<TSource, TDestination>, GenericCollectionTransformer<TSource, TDestination>>();
        }
    }
}
