using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Attributes
{
    public class ManageUserProfilesFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly IProfileService profileService;
        private ProfileModel profileModel = null;

        public ManageUserProfilesFilter(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var firstNameClaim = context.HttpContext.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
            profileModel = profileService.GetOrCreate(
                firstNameClaim.Value,
                context.HttpContext.User.Identity.Name);
            profileService.Dispose();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controller = context.Controller as Controller;
            if (controller != null && profileModel != null)
            {
                controller.ViewBag.UserProfile = profileModel;
            }
        }
    }
}
