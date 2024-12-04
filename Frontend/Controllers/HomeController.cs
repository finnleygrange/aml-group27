using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using TransactionService.Models;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5182/api");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Account()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/transactions");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<List<Transaction>>(content);
                return View(transactions);
            }

            TempData["Error"] = "Failed to fetch transactions.";
            return View(new List<Transaction>());
        }


        public IActionResult Search()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
