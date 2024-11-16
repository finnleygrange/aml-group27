using MediaService.Enums;

namespace MediaService.Models
{
    public class MediaItem
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsAvailable { get; set; }
        public virtual int BorrowDurationDays { get; set; }
        public MediaType MediaType { get; set; }
    }

    public enum MediaType
    {
        Book
    }
}
