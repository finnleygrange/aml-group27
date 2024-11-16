namespace MediaService.Models
{
    public class Book : MediaItem
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }

        public override int BorrowDurationDays
        {
            get { return 14; }
            set { base.BorrowDurationDays = value; }
        }
    }
}
