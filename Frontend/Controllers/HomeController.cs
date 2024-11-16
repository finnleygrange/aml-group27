using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;

            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://mediaservice:8001")
            };
        }

        public async Task<IActionResult> Weather()
        {
            var response = await _httpClient.GetAsync("WeatherForecast");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", $"Error: {response.StatusCode}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<List<WeatherForecast>>(jsonString);

            return View(weatherData);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
