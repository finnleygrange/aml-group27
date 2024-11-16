using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Frontend.Controllers
{
    public class TransactionController : Controller
    {
        private readonly HttpClient _httpClient;
        public TransactionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("http://localhost:5125/api/transaction");
            response.EnsureSuccessStatusCode();

            var mediaData = await response.Content.ReadAsStringAsync();
            ViewData["TransactionData"] = mediaData;

            return View();
        }
    }
}
