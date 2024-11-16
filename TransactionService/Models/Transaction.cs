namespace TransactionService.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsRenewed { get; set; }

        public bool IsOverdue
        {
            get
            {
                return ReturnDate == null && DateTime.Now > DueDate;
            }
        }
    }
}
