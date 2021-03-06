﻿using Meme_Platform.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Interfaces
{
    public interface IPostService : IServiceBase
    {
        Task<PostModel> PostImage(string title, byte[] data, string extension,
            string ownerIdentifier, bool isNsfw);

        Task<PostModel> PostYTVideo(string title, string videoUrl, string ownerIdentifier,
            bool isNsfw);

        IEnumerable<PostModel> GetPostsPage(int page);

        Task<PostModel> GetPostOfTheDay();

        IEnumerable<PostModel> GetTopPosts();

        IEnumerable<ProfileModel> GetTopContributors();

        Task Unvote(int postId, string voterIdentifier);

        Task Vote(int postId, string voterIdentifier, VoteType voteType);

        PostModel GetPost(int id);
    }
}
