using Meme_Platform.IL.Events;
using Meme_Platform.IL.Events.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.IL
{
    public static class Extensions
    {
        public static void BootstrapIntegrationLayer(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerStore, EventHandlerStore>();
            services.AddSingleton<IPluginStore, PluginStore>();

            services.AddTransient<IEventPublisher, EventPublisher>();
        }
    }
}
