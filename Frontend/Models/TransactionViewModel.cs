using TransactionService.Models;

namespace Frontend.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }
        public Transaction Transaction { get; set; }
    }

}
