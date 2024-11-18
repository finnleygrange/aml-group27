namespace MediaService.Models
{
    public class Journal : MediaItem
    {
        public string Publisher { get; set; }
        public int IssueNumber { get; set; }
    }
}
