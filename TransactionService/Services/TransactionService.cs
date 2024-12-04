using TransactionService.Data;
using TransactionService.Models;
using System.Linq;

namespace TransactionService.Services
{
    public class TransactionService
    {
        private readonly TransactionDbContext _context;

        public TransactionService(TransactionDbContext context)
        {
            _context = context;
        }

        public bool BorrowMedia(int mediaItemId)
        {
            var existingTransaction = _context.Transactions
                .FirstOrDefault(t => t.MediaItemId == mediaItemId && t.ReturnDate == null);

            if (existingTransaction != null)
            {
                return false;
            }

            var transaction = new Transaction
            {
                MediaItemId = mediaItemId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = "Borrowed"
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return true;
        }

        public bool RenewMedia(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == transactionId);

            if (transaction == null || transaction.ReturnDate != null)
            {
                return false;
            }

            transaction.DueDate = transaction.DueDate.AddDays(14);
            transaction.Status = "Renewed";
            _context.SaveChanges();

            return true;
        }

        public bool ReturnMedia(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == transactionId);

            if (transaction == null || transaction.ReturnDate != null)
            {
                return false;
            }

            transaction.ReturnDate = DateTime.Now;
            transaction.Status = "Returned";
            _context.SaveChanges();

            return true;
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }
    }
}
