using Meme_Platform.Attributes;
using Meme_Platform.Core.Models;
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

        [HttpPost]
        public IActionResult Update(ProfileModel model)
        {
            if (string.IsNullOrEmpty(model.Nickname) || model.Nickname.Length < 3)
            {
                return BadRequest("Nickname invalid!");
            }

            if (string.IsNullOrEmpty(model.ProfilePictureUrl) || model.ProfilePictureUrl.Length < 3)
            {
                return BadRequest("ProfilePictureUrl invalid!");
            }

            profileService.UpdateProfile(User.Identity.Name, model.Nickname, model.ProfilePictureUrl);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                profileService.Dispose();
            }
        }
    }
}