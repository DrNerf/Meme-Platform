using Meme_Platform.Core.Services.Interfaces;
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

        public ManageUserProfilesFilter(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var firstNameClaim = context.HttpContext.User.Claims.First(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");
            profileService.CreateIfMissing(firstNameClaim.Value, context.HttpContext.User.Identity.Name);
            profileService.Dispose();
        }
    }
}
