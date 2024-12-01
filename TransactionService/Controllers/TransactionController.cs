using Microsoft.AspNetCore.Mvc;

namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly Services.TransactionService _transactionService;

        public TransactionController(Services.TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("borrow")]
        public IActionResult Borrow(int userId, int mediaItemId)
        {
            bool result = _transactionService.BorrowMedia(userId, mediaItemId);
            if (result)
            {
                return Ok("Transaction successful.");
            }
            return BadRequest("Failed to borrow media.");
        }

        [HttpPost("renew")]
        public IActionResult Renew(int transactionId)
        {
            bool result = _transactionService.RenewMedia(transactionId);
            if (result)
            {
                return Ok("Transaction renewed.");
            }
            return BadRequest("Failed to renew media.");
        }

        [HttpPost("return")]
        public IActionResult Return(int transactionId)
        {
            bool result = _transactionService.ReturnMedia(transactionId);
            if (result)
            {
                return Ok("Transaction returned.");
            }
            return BadRequest("Failed to return media.");
        }
    }
}

