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

            // Configure TPT (Table-per-Type) inheritance
            modelBuilder.Entity<MediaItem>().ToTable("MediaItems");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<DVD>().ToTable("DVDs");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Journal>().ToTable("Journals");

            // Seed MediaType values
            modelBuilder.Entity<MediaType>().HasData(
                new MediaType { Id = 1, Name = "Book" },
                new MediaType { Id = 2, Name = "DVD" },
                new MediaType { Id = 3, Name = "Game" },
                new MediaType { Id = 4, Name = "Journal" },
                new MediaType { Id = 5, Name = "Multimedia" }
            );

            // Seed Book data
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Example Book",
                    Description = "An example book description.",
                    ImageUrl = "https://example.com/images/book.jpg",
                    MediaTypeId = 1,  // Reference MediaType table
                    Author = "John Doe",
                    ISBN = "123-456-789",
                    PageCount = 300
                }
            );

            // Seed DVD data
            modelBuilder.Entity<DVD>().HasData(
                new DVD
                {
                    Id = 2,
                    Title = "Example DVD",
                    Description = "An example DVD description.",
                    ImageUrl = "https://example.com/images/dvd.jpg",
                    MediaTypeId = 2,  // Reference MediaType table
                    Director = "Jane Smith",
                    DurationMinutes = 120,
                    ReleaseYear = 2020
                }
            );

            // Seed Game data
            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 3,
                    Title = "Example Game",
                    Description = "An example game description.",
                    ImageUrl = "https://example.com/images/game.jpg",
                    MediaTypeId = 3,  // Reference MediaType table
                    Platform = "PC",
                    Genre = "Action",
                    AgeRating = "Mature",
                    Developer = "GameDev Studios"
                }
            );

            // Seed Journal data
            modelBuilder.Entity<Journal>().HasData(
                new Journal
                {
                    Id = 4,
                    Title = "Example Journal",
                    Description = "An example journal description.",
                    ImageUrl = "https://example.com/images/journal.jpg",
                    MediaTypeId = 4,  // Reference MediaType table
                    Publisher = "Science Publishing",
                    IssueNumber = 12
                }
            );
        }
    }
}
