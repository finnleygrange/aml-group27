using MediaService.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaService.Data
{
    public class MediaServiceDbContext : DbContext
    {
        public DbSet<MediaItem> MediaItems { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<DVD> DVDs { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }

        public MediaServiceDbContext(DbContextOptions<MediaServiceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaItem>().ToTable("MediaItems");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<DVD>().ToTable("DVDs");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Journal>().ToTable("Journals");

            modelBuilder.Entity<MediaType>().HasData(
                new MediaType { Id = 1, Name = "Book" },
                new MediaType { Id = 2, Name = "DVD" },
                new MediaType { Id = 3, Name = "Game" },
                new MediaType { Id = 4, Name = "Journal" },
                new MediaType { Id = 5, Name = "Multimedia" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Example Book",
                    Description = "An example book description.",
                    MediaTypeId = 1,
                    Author = "John Doe",
                    Genre = "Fiction",
                    ISBN = "123-456-789",
                    PageCount = 300
                }
            );

            modelBuilder.Entity<DVD>().HasData(
                new DVD
                {
                    Id = 2,
                    Title = "Example DVD",
                    Description = "An example DVD description.",
                    MediaTypeId = 2,
                    Author = "Jane Doe",
                    Genre = "Documentary",
                    DurationMinutes = 120
                }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 3,
                    Title = "Example Game",
                    Description = "An example game description.",
                    MediaTypeId = 3,
                    Author = "Game Studio",
                    Genre = "Action",
                    Platform = "PC",
                    AgeRating = "Mature"
                }
            );

            modelBuilder.Entity<Journal>().HasData(
                new Journal
                {
                    Id = 4,
                    Title = "Example Journal",
                    Description = "An example journal description.",
                    MediaTypeId = 4,
                    Author = "Editor Team",
                    Genre = "Science",
                    IssueNumber = 12
                }
            );
        }
    }
}
