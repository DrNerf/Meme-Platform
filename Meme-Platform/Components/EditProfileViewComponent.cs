using Meme_Platform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Components
{
    public class EditProfileViewComponent : ViewComponent
    {
        private readonly IServiceProvider serviceProvider;

        public EditProfileViewComponent(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IViewComponentResult Invoke()
        {
            using var profileService = serviceProvider.GetService<IProfileService>();
            var profile = profileService.GetProfile(User.Identity.Name);
            return View(profile);
        }
    }
}
