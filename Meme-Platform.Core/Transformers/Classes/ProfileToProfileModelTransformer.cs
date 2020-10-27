using Meme_Platform.Core.Models;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL.Entities;
using System.Linq;

namespace Meme_Platform.Core.Transformers.Classes
{
    internal class ProfileToProfileModelTransformer : ITransformer<Profile, ProfileModel>
    {
        public ProfileModel Transform(Profile source)
        {
            return new ProfileModel
            {
                ProfilePictureUrl = source.ProfilePictureUrl,
                ADIdentifier = source.ADIdentifier,
                Nickname = source.Nickname,
                PostsCount = source.Posts.Count,
                VotesCount = source.Votes.Count,
                CommentsCount = source.Comments.Count,
                MaxScore = source.Posts.Select(p => p.Votes.Select(v => v.Type).Cast<int>().Sum())
                    .OrderByDescending(s => s).First()
            };
        }
    }
}
