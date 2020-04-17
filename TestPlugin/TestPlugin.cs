using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.IL;
using Meme_Platform.IL.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPlugin
{
    public class TestPlugin : IPlugin
    {
        public string GetDescription() => "Plugin used for testing and developing the Meme Platform's integration layer.";

        public string GetName() => "Test Plugin";

        public string GetVersion() => "1.0.0";
    }

    public class NewPostEventHandler : IPostCreatedEventHandler
    {
        private readonly ILogger<NewPostEventHandler> logger;
        private readonly ICommentService commentService;

        public NewPostEventHandler(ILogger<NewPostEventHandler> logger, ICommentService commentService)
        {
            this.logger = logger;
            this.commentService = commentService;
        }

        public void Execute(PostModel payload)
        {
            logger.LogInformation("Event handler fired!");
            commentService.Comment(payload.Id, "Test plugin was here *tips hat*", payload.Owner.ADIdentifier);
        }
    }
}
