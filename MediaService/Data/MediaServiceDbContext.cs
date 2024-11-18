using MediaService.Enums;
using MediaService.Models;
using Microsoft.EntityFrameworkCore;
using MediaType = MediaService.Models.MediaType;

namespace MediaService.Data
{
    public class MediaServiceDbContext : DbContext
    {
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<Models.MediaType> MediaTypes { get; set; }

        public MediaServiceDbContext(DbContextOptions<MediaServiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaType>().HasData(
               new MediaType { Id = 1, Name = "Book" },
               new MediaType { Id = 2, Name = "DVD" },
               new MediaType { Id = 3, Name = "Game" },
               new MediaType { Id = 4, Name = "Journal" },
               new MediaType { Id = 5, Name = "Multimedia" }
            );

            modelBuilder.Entity<MediaItem>().HasData(
                new MediaItem
                {
                    Id = 1,
                    Title = "Example Book",
                    Description = "An example book description.",
                    ImageUrl = "https://example.com/images/book.jpg",
                    MediaTypeId = 1
                },
                new MediaItem
                {
                    Id = 2,
                    Title = "Example DVD",
                    Description = "An example DVD description.",
                    ImageUrl = "https://example.com/images/dvd.jpg",
                    MediaTypeId = 2
                }
            );
        }
    }
}
