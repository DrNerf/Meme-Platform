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

        public CommentToCommentModelTransformer(
            ITransformer<Profile, ProfileModel> profileTransformer)
        {
            this.profileTransformer = profileTransformer;
        }

        public CommentModel Transform(Comment source)
        {
            return new CommentModel
            {
                DateTimePosted = source.DateTimePosted,
                Owner = profileTransformer.Transform(source.Owner),
                Text = source.Text,
                Parent = source.Parent != null ? Transform(source.Parent) : null,
                Comments = source.Comments.Select(c => Transform(c)).ToList()
            };
        }
    }
}
