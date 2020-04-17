using Meme_Platform.IL.Events;
using Meme_Platform.IL.Events.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.IL
{
    public static class Extensions
    {
        public static void BootstrapIntegrationLayer(
            this IServiceCollection services)
        {
            services.AddSingleton<IPluginStore, PluginStore>();
            services.AddSingleton<IEventHandlerStore, EventHandlerStore>();

            services.AddTransient<IEventPublisher, EventPublisher>();
        }
    }
}
