using System.Text.Json.Serialization;

namespace MediaService.Models
{
    public class MediaItem
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public int MediaTypeId { get; set; }
        public MediaType MediaType { get; set; }
    }
}
