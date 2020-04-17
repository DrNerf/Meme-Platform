namespace Meme_Platform.IL.Events
{
    public interface IEventPublisher
    {
        void Publish<TEventHandler, TPayload>(TPayload payload)
            where TEventHandler : IEventHandler<TPayload>;

        void PublishInBackground<TEventHandler, TPayload>(TPayload payload)
            where TEventHandler : IEventHandler<TPayload>;
    }
}
