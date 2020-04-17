using System;
using System.Collections.Generic;

namespace Meme_Platform.IL.Events
{
    public interface IEventHandlerStore
    {
        void RegisterHandler<TEventHandler>() where TEventHandler : class, IEventHandler;

        void RegisterHandler(Type eventHandlerType);

        public IEnumerable<TEventHandler> CreateHandlers<TEventHandler>() where TEventHandler : IEventHandler;
    }
}
