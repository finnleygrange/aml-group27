using MediaService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class DvdsController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("http://localhost:5150/api");

    public DvdsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet("/dvds")]
    public async Task<IActionResult> GetAllDvds(string searchQuery)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/media/dvds");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var dvds = JsonConvert.DeserializeObject<List<DVD>>(content);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                ViewData["SearchQuery"] = searchQuery;
                dvds = dvds.Where(d =>
                    (d.Title != null && d.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                    (d.Description != null && d.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            return View("Index", dvds);
        }
        return View("Error");
    }




    [HttpGet("/dvds/details/{id}")]
    public async Task<IActionResult> GetDvd(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/dvds/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var dvd = JsonConvert.DeserializeObject<DVD>(content);
            return View("Details", dvd);
        }
        return View("Error");
    }


    [HttpGet("/dvds/create")]
    public IActionResult CreateDvd()
    {
        return View("Create", new DVD());
    }

    [HttpPost("/dvds/create")]
    public async Task<IActionResult> CreateDvd(DVD dvd)
    {
        var content = new StringContent(JsonConvert.SerializeObject(dvd), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/media/dvds", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllDvds");  
        }
        return View("Error");
    }


    [HttpGet("/dvds/edit/{id}")]
    public async Task<IActionResult> EditDvd(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/dvds/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var dvd = JsonConvert.DeserializeObject<DVD>(content);
            return View("Edit", dvd); 
        }
        return View("Error");
    }

    [HttpPost("/dvds/edit/{id}")]
    public async Task<IActionResult> EditDvd(DVD dvd)
    {
        var content = new StringContent(JsonConvert.SerializeObject(dvd), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/media/dvds", content);

        if (response.IsSuccessStatusCode)
        {
        }
        return View("Error");
    }

    [HttpGet("/dvds/delete/{id}")]
    public async Task<IActionResult> DeleteDvd(int id)
    {
        var response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/media/dvds/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var dvd = JsonConvert.DeserializeObject<DVD>(content);
            return View("Delete", dvd);  
        }
        return View("Error");
    }

    [HttpPost("/dvds/delete/{id}/confirm")]
    public async Task<IActionResult> DeleteDvdConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"/media/dvds/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("GetAllDvds");
        }

        return View("Error");
    }
}
