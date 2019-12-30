using Meme_Platform.Core.Models;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface IContentService : IServiceBase
    {
        ContentModel Get(int id);
    }
}
