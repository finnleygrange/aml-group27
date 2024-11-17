using MediaService.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaService.Data
{
    public class MediaServiceDbContext : DbContext
    {
        public DbSet<MediaItem> MediaItems { get; set; }

        public MediaServiceDbContext(DbContextOptions<MediaServiceDbContext> options) : base(options)
        {
        }
    }
}
