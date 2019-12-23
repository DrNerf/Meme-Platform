using Meme_Platform.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meme_Platform.DAL
{
    internal class PlatformContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<PostOfTheDay> PostsOfTheDay { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost:5432;Database=meme-platform;Username=user;Password=user");
    }
}
