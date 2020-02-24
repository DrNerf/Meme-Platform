using Meme_Platform.Attributes;
using Meme_Platform.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Meme_Platform.Controllers
{

    [Authorize]
    [ServiceFilter(typeof(ManageUserProfilesFilter))]
    [ServiceFilter(typeof(DependencyInjectorFilter))]
    public abstract class ControllerBase : Controller
    {

    }
}
