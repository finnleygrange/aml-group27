namespace MediaService.Models
{
    public class Game : MediaItem
    {
        public string Platform { get; set; }
        public string Genre { get; set; }
        public string AgeRating { get; set; }
        public string Developer { get; set; }
    }
}
