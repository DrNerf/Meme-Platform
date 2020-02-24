using Meme_Platform.Attributes;
using Meme_Platform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meme_Platform.Controllers
{
    public class ProfileController : ControllerBase
    {
        [Inject]
        private readonly IProfileService profileService;

        [HttpGet]
        public IActionResult Avatar()
        {
            var profile = profileService.GetProfile(User.Identity.Name);
            if (profile != null)
            {
                return Redirect(profile.ProfilePictureUrl);
            }

            return NotFound();
        }
    }
}