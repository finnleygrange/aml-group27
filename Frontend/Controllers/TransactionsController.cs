using Azure;
using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransactionService.Models;

namespace Frontend.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseAddress = new Uri("http://localhost:5182/api");

        public TransactionsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = _baseAddress;
        }

        [HttpGet("/transactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/transactions");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<List<Transaction>>(content);

                return View("Index", transactions);
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult Borrow(int mediaItemId) 
        {
            return View(mediaItemId); 
        }

        [HttpPost]
        public async Task<IActionResult> BorrowMedia(int mediaItemId)
        {
            var response = await _httpClient.PostAsync($"transactions/borrow?mediaItemId={mediaItemId}", null);
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Media item borrowed successfully.";
                return RedirectToAction("GetAllTransactions");
            }

            TempData["Error"] = "Failed to borrow media.";
            return RedirectToAction("GetAllTransactions");
        }


    }
}
