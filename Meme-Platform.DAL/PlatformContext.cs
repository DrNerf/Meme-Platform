using Meme_Platform.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meme_Platform.DAL
{
    public class PlatformContext : DbContext
    {
        public PlatformContext(DbContextOptions<PlatformContext> options)
            :base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<PostOfTheDay> PostsOfTheDay { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
