using Meme_Platform.Core.Models;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL.Entities;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class ProfileToProfileModelTransformer : ITransformer<Profile, ProfileModel>
    {
        public ProfileModel Transform(Profile source)
        {
            return new ProfileModel 
            {
                ProfilePictureUrl = source.ProfilePictureUrl,
                ADIdentifier = source.ADIdentifier
            };
        }
    }
}
