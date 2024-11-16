using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

public class MediaController : Controller
{
    private readonly HttpClient _httpClient;

    public MediaController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("http://localhost:5150/api/media");
        response.EnsureSuccessStatusCode();

        var mediaData = await response.Content.ReadAsStringAsync();
        ViewData["MediaData"] = mediaData;

        return View();
    }
}
