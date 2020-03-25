using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meme_Platform.Core.Services.Classes
{
    internal abstract class ServiceBase : IServiceBase
    {
        private readonly IRepository<Profile> profileRepository;

        public ServiceBase(IRepository<Profile> profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        protected Profile GetProfile(string userIdentifier)
        {
           return profileRepository.Get().First(p => p.ADIdentifier == userIdentifier);
        }

        public virtual void Dispose()
        {
            profileRepository?.Dispose();
        }
    }
}
