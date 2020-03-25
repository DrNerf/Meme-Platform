using Meme_Platform.Attributes;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Meme_Platform.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ManageUserProfilesFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService postService;

        public HomeController(
            ILogger<HomeController> logger,
            IPostService postService)
        {
            _logger = logger;
            this.postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeViewModel
            {
                PostOfTheDay = await postService.GetPostOfTheDay(),
                TopContributors = postService.GetTopContributors(),
                TopPosts = postService.GetTopPosts()
            };

            return View(viewModel);
        }

        public IActionResult Posts(int page)
        {
            return ViewComponent("PostsPage", new { page });
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                postService?.Dispose();
            }
        }
    }
}
