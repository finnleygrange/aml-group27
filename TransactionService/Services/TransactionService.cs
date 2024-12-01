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

        public bool BorrowMedia(int userId, int mediaItemId)
        {
            var existingTransaction = _context.Transactions
                .FirstOrDefault(t => t.MediaItemId == mediaItemId && t.ReturnDate == null);

            if (existingTransaction != null)
            {
                return false; // If there’s already an active transaction for this media item
            }

            var transaction = new Transaction
            {
                UserId = userId.ToString(),
                MediaItemId = mediaItemId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14) // Borrow for 14 days
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
                return false; // Can't renew if transaction is already returned or doesn't exist
            }

            transaction.DueDate = transaction.DueDate.AddDays(14); // Extend by 14 days
            _context.SaveChanges();

            return true;
        }

        public bool ReturnMedia(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.Id == transactionId);

            if (transaction == null || transaction.ReturnDate != null)
            {
                return false; // Can't return if already returned or transaction doesn’t exist
            }

            transaction.ReturnDate = DateTime.Now; // Mark the media as returned
            _context.SaveChanges();

            return true;
        }
    }
}
