using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class MediaController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("http://localhost:5150/api");

    public MediaController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet("/media")]
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var mediaItems = JsonConvert.DeserializeObject<List<MediaItem>>(content);
            return View(mediaItems);
        }
        return View("Error");
    }
}
