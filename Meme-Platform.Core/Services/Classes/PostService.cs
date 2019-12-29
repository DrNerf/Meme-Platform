using Meme_Platform.Core.Models;
using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.Core.Transformers.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly ICollectionTransformer<Post, PostModel> postsTransformer;
        private readonly IRepository<PostOfTheDay> postOfTheDayRepository;
        private readonly ICollectionTransformer<Profile, ProfileModel> profilesTransformer;
        private readonly CoreConfig coreConfig;

        public PostService(
            IRepository<Post> postRepository,
            IRepository<Content> contentRepository,
            IRepository<Profile> profileRepository,
            ITransformer<Post, PostModel> postTransformer,
            ICollectionTransformer<Post, PostModel> postsTransformer,
            IRepository<PostOfTheDay> postOfTheDayRepository,
            ICollectionTransformer<Profile, ProfileModel> profilesTransformer,
            CoreConfig coreConfig)
        {
            this.postRepository = postRepository;
            this.contentRepository = contentRepository;
            this.profileRepository = profileRepository;
            this.postTransformer = postTransformer;
            this.postsTransformer = postsTransformer;
            this.postOfTheDayRepository = postOfTheDayRepository;
            this.profilesTransformer = profilesTransformer;
            this.coreConfig = coreConfig;
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

        public IEnumerable<PostModel> GetPostsPage(int page)
        {
            var posts = postRepository.Get()
                .OrderByDescending(p => p.DateCreated)
                .Skip(coreConfig.PageSize * (page - 1))
                .Take(coreConfig.PageSize)
                .ToList();

            return postsTransformer.Transform(posts);
        }

        public async Task<PostModel> GetPostOfTheDay()
        {
            var postOfTheDay = postOfTheDayRepository.Get()
                .FirstOrDefault(p => p.Date == DateTime.Now);
            if (postOfTheDay == null)
            {
                var randomPost = postRepository.Get()
                    .OrderBy(p => Guid.NewGuid())
                    .FirstOrDefault(p => p.Content.ContentType == DAL.Entities.ContentType.Image);
                if (randomPost != null)
                {
                    postOfTheDay = postOfTheDayRepository.Add(new PostOfTheDay
                    {
                        Date = DateTime.Now.Date,
                        Post = randomPost
                    });

                    await postOfTheDayRepository.SaveChangesAsync();
                }
            }

            return postTransformer.Transform(postOfTheDay.Post);
        }

        public IEnumerable<PostModel> GetTopPosts()
        {
            return postsTransformer.Transform(postRepository.Get()
                .OrderByDescending(p => p.Votes.Sum(v => (int)v.Type))
                .Take(coreConfig.TopStats)
                .ToList());
        }

        public IEnumerable<ProfileModel> GetTopContributors()
        {
            return profilesTransformer.Transform(profileRepository.Get()
                .OrderByDescending(u => u.Posts.Count())
                .Take(coreConfig.TopStats)
                .ToList());
        }

        public void Dispose()
        {
            postRepository?.Dispose();
            contentRepository?.Dispose();
            profileRepository?.Dispose();
        }
    }
}
