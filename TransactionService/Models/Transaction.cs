namespace TransactionService.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int MediaItemId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
