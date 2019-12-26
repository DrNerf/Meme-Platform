using Meme_Platform.Core.Models;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL.Entities;
using System;
using System.Linq;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class PostToPostModelTransformer : ITransformer<Post, PostModel>
    {
        private readonly ITransformer<Profile, ProfileModel> profileTransformer;
        private readonly ICollectionTransformer<Vote, VoteModel> votesTransformer;
        private readonly ITransformer<Content, ContentModel> contentTransformer;
        private readonly ICollectionTransformer<Comment, CommentModel> commentsTransfomer;

        public PostToPostModelTransformer(
            ITransformer<Profile, ProfileModel> profileTransformer,
            ICollectionTransformer<Vote, VoteModel> votesTransformer,
            ITransformer<Content, ContentModel> contentTransformer,
            ICollectionTransformer<Comment, CommentModel> commentsTransfomer)
        {
            this.profileTransformer = profileTransformer;
            this.votesTransformer = votesTransformer;
            this.contentTransformer = contentTransformer;
            this.commentsTransfomer = commentsTransfomer;
        }

        public PostModel Transform(Post source)
        {
            return new PostModel
            {
                DateCreated = source.DateCreated,
                Id = source.Id,
                IsNSFW = source.IsNSFW,
                Title = source.Title,
                Owner = profileTransformer.Transform(source.Owner),
                Votes = votesTransformer.Transform(source.Votes).ToList(),
                Content = contentTransformer.Transform(source.Content),
                Comments = commentsTransfomer.Transform(source.Comments).ToList()
            };
        }
    }
}
