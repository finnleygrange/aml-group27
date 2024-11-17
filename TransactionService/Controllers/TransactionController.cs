using Microsoft.AspNetCore.Mvc;

namespace TransactionService.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Action to borrow a media item (book)
        [HttpPost]
        public IActionResult Borrow(int userId, int mediaItemId)
        {
            bool result = _transactionService.BorrowMedia(userId, mediaItemId);
            if (result)
            {
                return RedirectToAction("Success"); // Redirect to a success page
            }

            return RedirectToAction("Error"); // Redirect to an error page
        }

        // Action to renew a media item (book)
        [HttpPost]
        public IActionResult Renew(int transactionId)
        {
            bool result = _transactionService.RenewMedia(transactionId);
            if (result)
            {
                return RedirectToAction("Success");
            }

            return RedirectToAction("Error");
        }

        // Action to return a media item (book)
        [HttpPost]
        public IActionResult Return(int transactionId)
        {
            bool result = _transactionService.ReturnMedia(transactionId);
            if (result)
            {
                return RedirectToAction("Success");
            }

            return RedirectToAction("Error");
        }
    }

}
