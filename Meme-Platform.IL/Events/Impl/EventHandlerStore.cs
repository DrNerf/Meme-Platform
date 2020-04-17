using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Meme_Platform.IL.Events.Impl
{
    internal class EventHandlerStore : IEventHandlerStore
    {
        private readonly Dictionary<Type, List<Type>> handlers = new Dictionary<Type, List<Type>>();
        private readonly IServiceProvider serviceProvider;

        public EventHandlerStore(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<TEventHandler> CreateHandlers<TEventHandler>()
            where TEventHandler : IEventHandler
        {
            List<Type> eventHandlerTypes = handlers.GetValueOrDefault(typeof(TEventHandler));
            IServiceScope serviceScope = serviceProvider.CreateScope();
            if (eventHandlerTypes != null)
            {
                return eventHandlerTypes.Select(type => ActivatorUtilities.CreateInstance(serviceScope.ServiceProvider, type))
                    .Cast<TEventHandler>();
            }

            return Enumerable.Empty<TEventHandler>();
        }

        public void RegisterHandler<TEventHandler>()
            where TEventHandler : class, IEventHandler
        {
            RegisterHandler(typeof(TEventHandler));
        }

        public void RegisterHandler(Type eventHandlerType)
        {
            var eligibleInterfaces = eventHandlerType.GetInterfaces()
                .Where(i => !i.Name.StartsWith("IEventHandler") && typeof(IEventHandler).IsAssignableFrom(i))
                .ToList();
            if (eligibleInterfaces.Count < 1)
            {
                throw new ArgumentException("Class does not implement any event handler!");
            }

            if (eligibleInterfaces.Count > 1)
            {
                throw new ArgumentException("Class implements more than one event handlers!");
            }

            Type eventHandlerInterfaceType = eligibleInterfaces.First();
            if (handlers.ContainsKey(eventHandlerInterfaceType))
            {
                handlers[eventHandlerInterfaceType].Add(eventHandlerType);
            }
            else
            {
                handlers.Add(eventHandlerInterfaceType, new List<Type> { eventHandlerType });
            }
        }
    }
}
