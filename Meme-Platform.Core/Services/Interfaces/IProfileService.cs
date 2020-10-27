using Meme_Platform.Core.Models;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface IProfileService : IServiceBase
    {
        ProfileModel GetProfile(string ADIndentifier);

        ProfileModel GetOrCreate(string nickname, string ADIdentifier);

        Task UpdateProfile(string ADIdentifier, string nickname, string profilePictureUrl);
    }
}
