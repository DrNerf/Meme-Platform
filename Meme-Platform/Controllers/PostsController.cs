using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Meme_Platform.Attributes;
using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.IL.Events;
using Meme_Platform.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meme_Platform.Controllers
{
    public class PostsController : ControllerBase
    {
        [Inject]
        private readonly IPostService postService;

        [Inject]
        private readonly ICommentService commentService;

        [Inject]
        private readonly IEventPublisher eventPublisher;

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var post = postService.GetPost(id);
            if (post != null)
            {
                return View(post); 
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Comment(int id, string message)
        {
            await commentService.Comment(id, message, User.Identity.Name);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int commentId, string message)
        {
            await commentService.Reply(commentId, message, User.Identity.Name);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadRequest request)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                return BadRequest("Invalid input data!");
            }

            PostModel post = null;
            if (request.Image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(stream);
                    var extension = Path.GetExtension(request.Image.FileName);
                    post = await postService.PostImage(request.Title, stream.ToArray(), extension, User.Identity.Name, request.IsNSFW);
                }
            }
            else if (!string.IsNullOrEmpty(request.youTubeLink))
            {
                post = await postService.PostYTVideo(request.Title, request.youTubeLink, User.Identity.Name, request.IsNSFW);
            }
            else
            {
                return BadRequest();
            }

            eventPublisher.Publish<IPostCreatedEventHandler, PostModel>(post);
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