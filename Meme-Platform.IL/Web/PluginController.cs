using Microsoft.AspNetCore.Mvc;

namespace Meme_Platform.IL.Web
{
    [Route("plugin/{controller}/{action}/{id?}")]
    public abstract class PluginController : Controller
    {
    }
}
