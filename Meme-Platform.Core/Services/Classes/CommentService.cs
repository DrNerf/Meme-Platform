using Meme_Platform.Core.Services.Interfaces;
using Meme_Platform.DAL;
using Meme_Platform.DAL.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meme_Platform.Core.Services.Classes
{
    internal class CommentService : ServiceBase, ICommentService
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Comment> commentRepository;

        public CommentService(
            IRepository<Profile> profileRepository,
            IRepository<Post> postRepository,
            IRepository<Comment> commentRepository)
            :base(profileRepository)
        {
            this.postRepository = postRepository;
            this.commentRepository = commentRepository;
        }

        public Task Comment(int postId, string message, string ownerIdentifier)
        {
            var owner = GetProfile(ownerIdentifier);
            var post = postRepository.Get().First(p => p.Id == postId);
            post.Comments.Add(new Comment
            {
                DateTimePosted = DateTime.Now,
                Owner = owner,
                Text = message
            });

            return postRepository.SaveChangesAsync();
        }

        public Task Reply(int commentId, string message, string ownerIdentifier)
        {
            var owner = GetProfile(ownerIdentifier);
            var comment = commentRepository.Get().First(c => c.Id == commentId);
            comment.Comments.Add(new Comment 
            {
                DateTimePosted = DateTime.Now,
                Owner = owner,
                Text = message
            });

            return postRepository.SaveChangesAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
            postRepository?.Dispose();
            commentRepository?.Dispose();
        }
    }
}
