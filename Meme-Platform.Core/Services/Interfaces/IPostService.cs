using Meme_Platform.Core.Models;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface IPostService : IServiceBase
    {
        public Task<PostModel> PostImage(string title, byte[] data, string extension,
            string ownerIdentifier, bool isNsfw);

        public Task<PostModel> PostYoutubeVideo(string title, byte[] data);
    }
}
