using Meme_Platform.Core.Models;

namespace Meme_Platform.IL.Events
{
    public interface IEventHandler
    {

    }

    public interface IEventHandler<TPayload>  : IEventHandler
    {
        void Execute(TPayload payload);
    }

    public interface IPostCreatedEventHandler : IEventHandler<PostModel> 
    {
    }

    public interface IPostCommentedEventHandler : IEventHandler<PostModel>
    {
    }

    public interface IPostVotedEventHandler : IEventHandler<PostModel>
    {
    }
}
