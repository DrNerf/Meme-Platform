using Meme_Platform.Attributes;
using Meme_Platform.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Meme_Platform.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(ManageUserProfilesFilter))]
    public class ContentController : Controller
    {
        private readonly IContentService contentService;
        private readonly ILogger<ContentController> logger;

        public ContentController(
            IContentService contentService,
            ILogger<ContentController> logger)
        {
            this.contentService = contentService;
            this.logger = logger;
        }

        [Route("image/{id}.{extension}")]
        [HttpGet]
        public IActionResult GetImage(int id, string extension)
        {
            try
            {
                return new FileStreamResult(new MemoryStream(contentService.Get(id).Data), $"image/{extension}");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Image with ID: {id} not found!");
                return NotFound("Whoops, Image not found.");
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                contentService?.Dispose();
            }
        }
    }
}