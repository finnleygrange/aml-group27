using System.Transactions;

namespace TransactionService.Services
{
    public class TransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Borrow Media (create a new transaction)
        public bool BorrowMedia(int userId, int mediaItemId)
        {
            var mediaItem = _context.MediaItems.FirstOrDefault(x => x.Id == mediaItemId);
            if (mediaItem == null || !mediaItem.IsAvailable)
            {
                return false; // Media not found or not available
            }

            // Create a new transaction for borrowing
            var transaction = new Transaction
            {
                UserId = userId.ToString(),
                MediaItemId = mediaItemId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(mediaItem.BorrowDurationDays)
            };

            mediaItem.IsAvailable = false; // Mark media as unavailable

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return true;
        }

        // Renew Media (extend the due date)
        public bool RenewMedia(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
            if (transaction == null || transaction.ReturnDate.HasValue)
            {
                return false; // Cannot renew if already returned
            }

            var mediaItem = _context.MediaItems.FirstOrDefault(x => x.Id == transaction.MediaItemId);
            if (mediaItem == null)
            {
                return false; // Media not found
            }

            transaction.DueDate = DateTime.Now.AddDays(mediaItem.BorrowDurationDays); // Extend due date
            _context.SaveChanges();

            return true;
        }

        // Return Media (mark the item as returned)
        public bool ReturnMedia(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
            if (transaction == null || transaction.ReturnDate.HasValue)
            {
                return false; // Cannot return if already returned
            }

            var mediaItem = _context.MediaItems.FirstOrDefault(x => x.Id == transaction.MediaItemId);
            if (mediaItem == null)
            {
                return false; // Media not found
            }

            transaction.ReturnDate = DateTime.Now; // Mark as returned
            mediaItem.IsAvailable = true; // Mark media as available again

            _context.SaveChanges();

            return true;
        }
    }

}
