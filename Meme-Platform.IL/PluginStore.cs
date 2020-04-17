using Meme_Platform.IL.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Meme_Platform.IL
{
    internal class PluginStore : IPluginStore
    {
        private readonly HashSet<IPlugin> plugins = new HashSet<IPlugin>();
        private readonly IEventHandlerStore eventHandlerStore;
        private readonly ILogger<PluginStore> logger;

        public PluginStore(IEventHandlerStore eventHandlerStore, ILogger<PluginStore> logger)
        {
            this.eventHandlerStore = eventHandlerStore;
            this.logger = logger;
        }

        public void RegisterPlugin(IPlugin plugin)
        {
            if (plugins.Add(plugin))
            {
                var pluginType = plugin.GetType();
                logger.LogInformation($"Plugin added to store: {pluginType.Name}");
                var assembly = pluginType.Assembly;
                IEnumerable<Type> eventHandlerTypes = assembly.GetTypes()
                    .Where(t => typeof(IEventHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                foreach (var eventHandlerType in eventHandlerTypes)
                {
                    eventHandlerStore.RegisterHandler(eventHandlerType);
                    logger.LogInformation($"Event handler {eventHandlerType.Name} registered for plugin {pluginType.Name}");
                }
            }
        }

        public IEnumerable<IPlugin> ScanForPlugins(string directory)
        {
            var dllFiles = Directory.GetFiles(directory, "*.dll");
            List<IPlugin> loadedPlugins = new List<IPlugin>();
            foreach (var dll in dllFiles)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
                IEnumerable<Type> pluginTypes = assembly.GetTypes()
                    .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                if (pluginTypes.Any())
                {
                    foreach (var plugin in pluginTypes)
                    {
                        try
                        {
                            loadedPlugins.Add(Activator.CreateInstance(plugin) as IPlugin);
                            logger.LogInformation($"Plugin loaded: {plugin.FullName}");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Could not load plugin: {plugin.FullName}");
                        }
                    }
                }
            }

            return loadedPlugins;
        }
    }

    public interface IPluginStore
    {
        void RegisterPlugin(IPlugin plugin);

        IEnumerable<IPlugin> ScanForPlugins(string directory);
    }
}
