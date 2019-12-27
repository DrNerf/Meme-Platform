using Meme_Platform.Core.Models;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface IProfileService : IServiceBase
    {
        ProfileModel GetProfile(string ADIndentifier);

        void CreateIfMissing(string nickname, string ADIdentifier);
    }
}
