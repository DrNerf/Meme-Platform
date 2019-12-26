using Meme_Platform.Core.Models;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class CommentToCommentModelTransformer : ITransformer<Comment, CommentModel>
    {
        private readonly ITransformer<Profile, ProfileModel> profileTransformer;
        private readonly ITransformer<Post, PostModel> postTransformer;
        private readonly ICollectionTransformer<Comment, CommentModel> commentsTransformer;

        public CommentToCommentModelTransformer(
            ITransformer<Profile, ProfileModel> profileTransformer,
            ITransformer<Post, PostModel> postTransformer,
            ICollectionTransformer<Comment, CommentModel> commentsTransformer)
        {
            this.profileTransformer = profileTransformer;
            this.postTransformer = postTransformer;
            this.commentsTransformer = commentsTransformer;
        }

        public CommentModel Transform(Comment source)
        {
            return new CommentModel
            {
                DateTimePosted = source.DateTimePosted,
                Owner = profileTransformer.Transform(source.Owner),
                Text = source.Text,
                Parent = source.Parent != null ? Transform(source.Parent) : null,
                PostOwner = postTransformer.Transform(source.PostOwner),
                Comments = commentsTransformer.Transform(source.Comments).ToList()
            };
        }
    }
}
