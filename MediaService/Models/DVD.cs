namespace MediaService.Models
{
    public class DVD : MediaItem
    {
        public string Director { get; set; }
        public int DurationMinutes { get; set; }
        public int ReleaseYear { get; set; }
    }
}
