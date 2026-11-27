using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mushroom_Finder.Models;

namespace Mushroom_Finder.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<MushroomEntity> Mushrooms => Set<MushroomEntity>();

        public DbSet<ForumPost> ForumPosts => Set<ForumPost>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ForumPost>()
                .HasOne(post => post.Author)
                .WithMany()
                .HasForeignKey(post => post.AuthorId)
                .IsRequired();
        }
    }
}
