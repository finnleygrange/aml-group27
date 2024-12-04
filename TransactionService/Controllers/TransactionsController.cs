using Microsoft.AspNetCore.Mvc;
using TransactionService.Models;

namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly Services.TransactionService _transactionService;

        public TransactionsController(Services.TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("borrow")]
        public IActionResult Borrow(int mediaItemId)
        {
            bool result = _transactionService.BorrowMedia(mediaItemId);
            if (result)
            {
                return Ok("Media item borrowed successfully.");
            }
            return BadRequest("Failed to borrow media.");
        }

        [HttpPost("renew")]
        public IActionResult Renew(int transactionId)
        {
            bool result = _transactionService.RenewMedia(transactionId);
            if (result)
            {
                return Ok("Media item renewed successfully.");
            }
            return BadRequest("Failed to renew media.");
        }

        [HttpPost("return")]
        public IActionResult Return(int transactionId)
        {
            bool result = _transactionService.ReturnMedia(transactionId);
            if (result)
            {
                return Ok("Media item returned successfully.");
            }
            return BadRequest("Failed to return media.");
        }

        [HttpGet]
        public IActionResult GetAllTransactions()
        {
            var transactions = _transactionService.GetAllTransactions(); 
            return Ok(transactions);
        }
    }
}
