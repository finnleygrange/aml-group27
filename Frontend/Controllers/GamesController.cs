using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class GamesController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("http://localhost:5150/api");

    public GamesController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet("/games")]
    public async Task<IActionResult> GetAllGames()
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media/games");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var games = JsonConvert.DeserializeObject<List<Game>>(content);
            return View("Index", games);  
        }
        return View("Error");  
    }


    [HttpGet("/games/{id}")]
    public async Task<IActionResult> GetGame(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/games/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var game = JsonConvert.DeserializeObject<Game>(content);
            return View("Details", game); 
        }
        return View("Error");  
    }


    [HttpGet("/games/create")]
    public IActionResult CreateGame()
    {
        return View("Create", new Game()); 
    }

    [HttpPost("/games/create")]
    public async Task<IActionResult> CreateGame(Game game)
    {
        var content = new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/media/games", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllGames"); 
        }
        return View("Error"); 
    }

    [HttpGet("/games/edit/{id}")]
    public async Task<IActionResult> EditGame(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/games/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var game = JsonConvert.DeserializeObject<Game>(content);
            return View("Edit", game);  
        }
        return View("Error");  
    }

    [HttpPost("/games/edit/{id}")]
    public async Task<IActionResult> EditGame(Game game)
    {
        var content = new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/media/games", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllGames");  
        }
        return View("Error"); 
    }

    [HttpGet("/games/delete/{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/games/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var game = JsonConvert.DeserializeObject<Game>(content);
            return View("Delete", game);  
        }
        return View("Error"); 
    }

    [HttpPost("/games/delete/{id}/confirm")]
    public async Task<IActionResult> DeleteGameConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/media/games/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllGames");  
        }
        return View("Error");
    }
}
