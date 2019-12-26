using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Classes
{
    internal class PostService : IPostService
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Content> contentRepository;
        private readonly IRepository<Profile> profileRepository;
        private readonly ITransformer<Post, PostModel> postTransformer;

        public PostService(
            IRepository<Post> postRepository,
            IRepository<Content> contentRepository,
            IRepository<Profile> profileRepository,
            ITransformer<Post, PostModel> postTransformer)
        {
            this.postRepository = postRepository;
            this.contentRepository = contentRepository;
            this.profileRepository = profileRepository;
            this.postTransformer = postTransformer;
        }

        public async Task<PostModel> PostImage(string title, byte[] data, string extension,
            string ownerIdentifier, bool isNsfw)
        {
            var owner = profileRepository.Get().First(p => p.ADIdentifier == ownerIdentifier);
            var content = contentRepository.Add(new Content
            {
                ContentType = DAL.Entities.ContentType.Image,
                Data = data,
                Extension = extension
            });

            var post = postRepository.Add(new Post 
            {
                Title = title,
                Content = content,
                IsNSFW = isNsfw,
                DateCreated = DateTime.Now,
                Owner = owner
            });

            post.Votes.Add(new Vote { Post = post, Type = DAL.Entities.VoteType.Up, Voter = owner });
            await postRepository.SaveChangesAsync();
            return postTransformer.Transform(post);
        }

        public Task<PostModel> PostYoutubeVideo(string title, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            postRepository?.Dispose();
            contentRepository?.Dispose();
            profileRepository?.Dispose();
        }
    }
}
