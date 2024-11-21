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
    public async Task<IActionResult> Index(string searchQuery)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var mediaItems = JsonConvert.DeserializeObject<List<MediaItem>>(content);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                ViewData["SearchQuery"] = searchQuery;
                mediaItems = mediaItems.Where(m =>
                    (m.Title != null && m.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                    (m.Description != null && m.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return View(mediaItems);
        }
        return View("Error");
    }
}
