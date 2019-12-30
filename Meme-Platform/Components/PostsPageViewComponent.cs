using Meme_Platform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Components
{
    public class PostsPageViewComponent : ViewComponent
    {
        private readonly IPostService postService;

        public PostsPageViewComponent(IPostService postService)
        {
            this.postService = postService;
        }

        public IViewComponentResult Invoke(int page)
        {
            return View(postService.GetPostsPage(page));
        }
    }
}
