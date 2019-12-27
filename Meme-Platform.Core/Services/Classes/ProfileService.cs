using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using System.Linq;

namespace Meme_Platform.Core.Services.Classes
{
    internal class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> profileRepository;
        private readonly ITransformer<Profile, ProfileModel> profileTransformer;

        public ProfileService(
            IRepository<Profile> profileRepository,
            ITransformer<Profile, ProfileModel> profileTransformer)
        {
            this.profileRepository = profileRepository;
            this.profileTransformer = profileTransformer;
        }

        public ProfileModel GetProfile(string ADIndentifier)
        {
            var profile = profileRepository.Get().FirstOrDefault(p => p.ADIdentifier == ADIndentifier);
            return profile == null ? null : profileTransformer.Transform(profile);
        }

        public ProfileModel GetOrCreate(string nickname, string ADIdentifier)
        {
            var profile = GetProfile(ADIdentifier);
            if (profile == null)
            {
                var dbProfile = profileRepository.Add(new Profile
                {
                    ADIdentifier = ADIdentifier,
                    ProfilePictureUrl = "~/img/Doge.png",
                    Nickname = nickname
                });

                profileRepository.SaveChanges();
                profile = profileTransformer.Transform(dbProfile);
            }

            return profile;
        }

        public void Dispose()
        {
            profileRepository?.Dispose();
        }
    }
}
