using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meme_Platform.IL.Events.Impl
{
    internal class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> logger;
        private readonly IEventHandlerStore eventHandlerStore;

        public EventPublisher(ILogger<EventPublisher> logger, IEventHandlerStore eventHandlerStore)
        {
            this.logger = logger;
            this.eventHandlerStore = eventHandlerStore;
        }

        public void Publish<TEventHandler, TPayload>(TPayload payload)
            where TEventHandler : IEventHandler<TPayload>
        {
            var handlers = eventHandlerStore.CreateHandlers<TEventHandler>();
            foreach (var handler in handlers)
            {
                try
                {
                    handler.Execute(payload);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, $"Failed to publish event: {typeof(TEventHandler).FullName}");
                }
            }
        }

        public void PublishInBackground<TEventHandler, TPayload>(TPayload payload)
            where TEventHandler : IEventHandler<TPayload>
        {
            Task.Run(() => Publish<TEventHandler, TPayload>(payload));
        }
    }
}
