using Meme_Platform.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meme_Platform.DAL
{
    public class PlatformContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<PostOfTheDay> PostsOfTheDay { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=meme-platform;Username=user;Password=user");
        }
    }
}
