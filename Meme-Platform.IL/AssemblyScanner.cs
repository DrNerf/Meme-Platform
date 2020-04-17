using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Meme_Platform.IL
{
    public static class AssemblyScanner
    {
        public static IEnumerable<IPlugin> ScanForPlugins(string directory, ILogger logger)
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
}
