using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Meme_Platform.Attributes;
using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meme_Platform.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ManageUserProfilesFilter))]
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(
            string title,
            IFormFile image,
            string youTubeLink,
            bool isNSFW)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Invalid input data!");
            }

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                var extension = Path.GetExtension(image.FileName);
                await postService.PostImage(title, stream.ToArray(), extension, User.Identity.Name, isNSFW);
            }

            // Don't await so we dont slow down the upload.
            // TODO: fix slack hook
            //SlackHelper.SendNotification(dbPost, GetCurrentWebsiteRoot());

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Unvote(int id)
        {
            await postService.Unvote(id, User.Identity.Name);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int id, VoteType voteType)
        {
            await postService.Vote(id, User.Identity.Name, voteType);
            return Ok();
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