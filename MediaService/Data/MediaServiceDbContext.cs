using MediaService.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaService.Data
{
    public class MediaServiceDbContext : DbContext
    {
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<Book> Books { get; set; }

        public MediaServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .HasBaseType<MediaItem>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
