using Meme_Platform.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface IPostService : IServiceBase
    {
        Task<PostModel> PostImage(string title, byte[] data, string extension,
            string ownerIdentifier, bool isNsfw);

        Task<PostModel> PostYoutubeVideo(string title, byte[] data);

        IEnumerable<PostModel> GetPostsPage(int page);

        Task<PostModel> GetPostOfTheDay();

        IEnumerable<PostModel> GetTopPosts();

        IEnumerable<ProfileModel> GetTopContributors();
    }
}
