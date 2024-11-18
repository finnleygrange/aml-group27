namespace MediaService.Models
{
    public class Book : MediaItem
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PageCount { get; set; }
    }
}
